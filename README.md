**C# .NET Core** приложение для получения размеров и последних дат обновления базы данных **PostgreSQL** серверов. Приложение создаёт таблицы в **Google Sheets** в которых выводятся все данные.

Использованы следующие **NuGet**-пакеты:
- **NPGSQL** - [open-source .NET провайдер данных для PostgreSQL](https://www.npgsql.org/doc/index.html)
- **Newtonsoft.Json** - [JSON - framework для .NET](https://www.newtonsoft.com/json/help/html/Introduction.htm)
- **Google.Apis.Sheets.v4** - [Библиотека для работы с Sheets v4 Google API](https://developers.google.com/sheets/api/guides/concepts)

Для работы с приложением необходимо заполнить файл настроек **appsettings.json**.

Пример **appsettings.json**
```
{
  "dbconnections": [
    {
      "db_name": "Имя вашего сервера",
      "connection_string": "Строка подключения к бд",
      "max_size": Размер вашего диска в гигабайте
    },
    {
      "db_name": "Имя вашего сервера",
      "connection_string": "Строка подключения к бд",
      "max_size": Размер вашего диска в гигабайтах
    },
  ],
  "googleconnection": {
    "clientID": "Id вашего апи",
    "clientSecret": "Секретный ключ апи",
    "sheetId": "Айди таблицы берётся из docs.google.com/spreadsheets/d/==>айди таблицы<==/edit#gid=1651728445"
  },
  "timeoffset": { ваш период обновления
    "days": 1,
    "hours": 1,
    "minutes": 1,
    "seconds": 1
  }
}
```

При этом менять ключ и секрет Апи ***совершенно не обязательно***, хотя мы можете создать свой апи на этой [странице](https://developers.google.com/sheets/api/quickstart/dotnet) 
