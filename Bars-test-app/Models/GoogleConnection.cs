using System;
using System.Collections.Generic;
using System.Text;

namespace Bars_test_app.Models
{
    /// <summary>
    /// Класс хранящий в себе все необходимые данные для работы с Google Sheets Api
    /// </summary>
    public class GoogleConnection
    {
        /// <summary>
        /// Айди вашей таблицы,легко узнаётся из ссылки на таблицу: docs.google.com/spreadsheets/d/{ваш айди}/edit#gid=0
        /// </summary>
        public string SheetID { get; set; }
        /// <summary>
        /// Если вы желаете использовать собственный апи зайдите на <see href="https://developers.google.com/sheets/api/quickstart/dotnet"/>
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// Если вы желаете использовать собственный апи зайдите на <see href="https://developers.google.com/sheets/api/quickstart/dotnet"/>
        /// </summary>
        public string ClientSecret { get; set; }
    }
}
