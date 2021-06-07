using System;
using System.Collections.Generic;

namespace MONQTest.Models
{
    ///<summary>
    ///Модель сущности письма
    ///</summary>>
    public class Mail
    {
        ///<summary>
        ///Идентификатор письма
        ///</summary>>
        public int ID { get; set; }
        ///<summary>
        ///Тема письма
        ///</summary>>
        public string Subject { get; set; }
        ///<summary>
        ///Тело письма
        ///</summary>>
        public string Body { get; set; }
        ///<summary>
        ///Список получателей, который нужен для связи многие ко многим с сущностью Recipient
        ///</summary>>
        public List<Recipient> Recipients { get; set; } = new List<Recipient>();
        ///<summary>
        ///Дата и время отправки письма
        ///</summary>>
        public DateTime DateTime { get; set; }
        ///<summary>
        ///Результат отправки сообщения
        ///</summary>>
        public Result Result { get; set; }
        ///<summary>
        ///Описание ошибки при отправки сообщения
        ///</summary>>
        public string FailedMessage { get; set; }
    }

    public enum Result
    { OK, Failed }
}
