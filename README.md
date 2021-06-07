Задача:
Требуется разработать web-сервис, задача которого формировать и отправлять письма
адресатам и логировать результат в БД.
1. Web-сервис должен принимать POST запрос по url: /api/mails/. Тело запроса в
формате json. Модель запроса прикладывается:
{
"subject": "string",
"body": "string",
"recipients": [ "string" ]
}
2. Метод обработки должен:
2.1. Сформировать email сообщение, выполнить его отправку.
2.2. Добавить запись в БД. В записи указать все поля, которые пришли в сообщении,
дату создания и результат отправки в виде поля Result: (значения OK, Failed), а также
поля FailedMessage (должно быть пустым или содержать ошибку отсылки
уведомления).
3. Web-сервис должен отвечать на GET запросы по url /api/mails/. В результате запроса
на этот url требуется выдать список всех отправленных сообщений (сохраненных в
БД), включая поля с п.2.2. в формате json.
4. Требуется написать комментарии на все public свойства и методы, придерживаясь
XML Documentation Comments (https://msdn.microsoft.com/en-us/library/b2s063f7.aspx)
5. Конфигурацию SMTP сервера вынести в файл конфигурации. (Не нужно указывать
реальные настройки вашего GMAIL аккаунта или SMTP релея!).
6. Разработку сервиса выполнить на c# .NET. В Visual Studio 2017+ (2017 и 2019 есть
Community Edition), Rider, либо в Visual Studio Code (для linux).
7. Для разработки сервиса использовать ASP.NET Core любой версии.
8. В качестве СУБД можно использовать любую реляционную (PgSQL, MySQL, MS
SQL, или другую, с которой знакомы).
9. В качестве ORM фреймворка можно использовать Entity Framework или Dapper.
Если используется Dapper, то схему БД выдать в виде SQL скрипта (CREATE
DATABASE…). Для Entity Framework требуется наличие миграции БД.
10. Библиотеку для отправки сообщений выбрать на свое усмотрение.
