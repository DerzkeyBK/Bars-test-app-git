using Google.Apis.Auth.OAuth2;

namespace Bars_test_app.Models
{
    /// <summary>
    /// Модель для десерализации настроек из json-файла
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        public Connection[] DBConnections { get; set; }
        /// <summary>
        /// Строка подключения к Google Sheets API
        /// </summary>
        public GoogleConnection GoogleConnection { get; set; }

        /// <summary>
        /// Период обновления таблицы
        /// </summary>
        public TimeOffset TimeOffset { get; set; }
    }
}
