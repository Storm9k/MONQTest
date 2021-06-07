using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text.Json;
using MONQTest.Models;
using MONQTest.Infrastructure;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace MONQTest.Controllers
{
    [Route("[controller]/mails")]
    [ApiController]
    public class API : ControllerBase
    {
        private AppDBContext DBContext;
        private IConfiguration Configuration;

        ///<summary>
        ///Получение зависимостей с помощью DI
        ///</summary>>
        public API(AppDBContext dBContext, IConfiguration configuration)
        {
            DBContext = dBContext;
            Configuration = configuration;
        }

        ///<summary>
        ///POST-метод, отвечающий за отправку письма
        ///</summary>>
        [HttpPost]
        public IActionResult Mails (Message message)
        {
            if (message != null)
            {
                ///<summary>
                ///Объявление объектов и присвоение первоначальных данных из объекта Message в Mail
                ///</summary>>
                MimeMessage mime = new MimeMessage();
                Mail mail = new Mail();
                mail.Subject = message.Subject;
                mail.Body = message.Body;
                mail.DateTime = mime.Date.DateTime;

                ///<summary>
                ///Присвоение данных письма отправителя
                ///</summary>>
                mime.From.Add(new MailboxAddress(Configuration["ConnectionStrings:SMTPConnection:Username"], Configuration["ConnectionStrings:SMTPConnection:Email"]));

                ///<summary>
                ///Присвоение данных письма для получателей и проверка на ввод
                ///</summary>>
                foreach (string address in message.Recipients)
                {
                    try
                    {
                        mime.To.Add(MailboxAddress.Parse(address));
                        Recipient recipient = DBContext.Recipients.FirstOrDefault(r => r.address == address);
                        if (recipient == null)
                        {
                            DBContext.Recipients.Add(new Recipient { address = address });
                            mail.Recipients.Add(new Recipient { address = address});
                        }
                        else
                        {
                            mail.Recipients.Add(recipient);
                        }
                    }
                    catch(Exception ex)
                    {
                        mail.FailedMessage += ex.Message;
                        mail.Result = Result.Failed;
                        DBContext.Mails.Add(mail);
                        return BadRequest();
                    }
                    finally
                    {
                        DBContext.SaveChanges();
                    }
                }

                ///<summary>
                ///Присвоение данных темы письма и проверка их на ввод
                ///</summary>>
                if (message.Subject != null)
                {
                    mime.Subject = message.Subject;
                }
                else
                {
                    mail.FailedMessage += "Empty subject";
                    mail.Result = Result.Failed;
                    DBContext.Mails.Add(mail);
                    DBContext.SaveChanges();
                    return BadRequest();
                }

                ///<summary>
                ///Присвоение данных тела письма и проверка их на ввод
                ///</summary>>
                if (message.Body != null)
                {
                    mime.Body = new TextPart("plain")
                    {
                        Text = message.Body
                    };
                }
                else
                {
                    mail.FailedMessage += "Empty Body";
                    mail.Result = Result.Failed;
                    DBContext.Mails.Add(mail);
                    DBContext.SaveChanges();
                    return BadRequest();
                }

                ///<summary>
                ///Создание объекта SMTP-клиента, ввод данных подключения
                ///</summary>>
                using (var client = new SmtpClient())
                {
                    try
                    {
                        client.Connect(Configuration["ConnectionStrings:SMTPConnection:Server"],
                                       Configuration.GetValue<int>("ConnectionStrings:SMTPConnection:Port"),
                                       false);
                        client.Authenticate(Configuration["ConnectionStrings:SMTPConnection:Email"],
                                            Configuration["ConnectionStrings:SMTPConnection:Password"]);
                        //client.Connect("smtp.yandex.ru", 465, true);
                        //client.Authenticate("", "");
                        client.Send(mime);
                        client.Disconnect(true);
                    }
                    catch (Exception ex)
                    {
                        mail.FailedMessage += ex.Message;
                        mail.Result = Result.Failed;
                        DBContext.Mails.Add(mail);
                        DBContext.SaveChanges();
                        return BadRequest();
                    }
                    
                }
                mail.Result = Result.OK;
                DBContext.Mails.Add(mail);
                DBContext.SaveChanges();
                return Ok();
            }
            else return BadRequest();
        }

        ///<summary>
        ///GET-метод, выдающий список всех писем в формате JSON
        ///</summary>>
        [HttpGet]
        public JsonResult Mails()
        {
            ///<summary>
            ///Настройка параметров сериализации для вывода данных на кириллице
            ///</summary>>
            var serialize_option = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            return new JsonResult(DBContext.Mails, serialize_option);
        }

    }
}
