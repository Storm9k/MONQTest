namespace MONQTest.Models
{
    ///<summary>
    ///Модель объекта сообщения, принимаемого в теле POST-запроса
    ///</summary>>
    public class Message
    {
        ///<summary>
        ///Тема письма
        ///</summary>>
        public string Subject { get; set; }
        ///<summary>
        ///Текст письма
        ///</summary>>
        public string Body { get; set; }
        ///<summary>
        ///Массив получателей письма
        ///</summary>>
        public string[] Recipients { get; set; }
    }
}
