using System.Collections.Generic;

namespace MONQTest.Models
{
    ///<summary>
    ///Модель получателя
    ///</summary>>
    public class Recipient
    {
        ///<summary>
        ///Идентификатор получателя
        ///</summary>>
        public int ID { get; set; }
        ///<summary>
        ///Email адрес получателя
        ///</summary>>
        public string address { get; set; }
        ///<summary>
        ///Список писем, который нужен для связи многие ко многим с сущностью Mail
        ///</summary>>
        public List<Mail> Mails { get; set; } = new List<Mail>();
    }
}
