using System;
using System.Collections.Generic;
using System.Text;

namespace Bars_test_app.Models
{
    /// <summary>
    /// Модель настроек для периода обновления
    /// </summary>
    public class TimeOffset
    {
        /// <summary>
        /// Дни
        /// </summary>
        public int Days { get; set; }
        /// <summary>
        /// Часы
        /// </summary>
        public int Hours { get; set; }
        /// <summary>
        /// Минуты
        /// </summary>
        public int Minutes { get; set; }
        /// <summary>
        /// Секунды
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// Переводит TimeOffset в TimeSpan
        /// </summary>
        /// <returns></returns>
        public TimeSpan ToTimespan()
        {
            return new TimeSpan(Days, Hours, Minutes, Seconds);
        }
    }
}
