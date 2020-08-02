using System;
using System.Collections.Generic;
using System.Text;

namespace Bars_test_app.Models
{
    /// <summary>
    /// Модель подключения к серверу
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// Имя сервера базы данных
        /// </summary>
        public string Db_Name { get; set; }
        /// <summary>
        /// Строка подклчения
        /// </summary>
        public string Connection_string { get; set; }
        /// <summary>
        /// Объём диска отведённый под сервер в GB
        /// </summary>
        public int Max_Size { get; set; }
    }
}
