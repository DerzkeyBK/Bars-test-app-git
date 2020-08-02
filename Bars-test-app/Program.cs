using Bars_test_app.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Bars_test_app
{
    /// <summary>
    /// Статический класс для хранения настроек и даты последнего обновления
    /// </summary>
    public static class Cache
    {
        /// <summary>
        /// Настройки
        /// </summary>
        public static Settings Settings { get; set; }
        /// <summary>
        /// Дата последнего обновления
        /// </summary>
        public static DateTime LastUpdate { get; set; }
        
        public static bool IsItTime()
        {
            return (LastUpdate - DateTime.Now) > Settings.TimeOffset.ToTimespan();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //считываем настройки
            using (StreamReader r = new StreamReader("appsettings.json"))
            {
                string json = r.ReadToEnd();
                Cache.Settings = JsonConvert.DeserializeObject<Settings>(json);
            }

            //первая итерация
            var TableData = SqlWorker.getData();
            GoogleTableWorker.SetDataToTable(TableData);
            Cache.LastUpdate = DateTime.Now;

            //все остальные
            while (true)
            {
                if(!Cache.IsItTime()) continue;

                TableData = SqlWorker.getData();
                GoogleTableWorker.SetDataToTable(TableData);
                Cache.LastUpdate = DateTime.Now;
            }
        }
    }
}
