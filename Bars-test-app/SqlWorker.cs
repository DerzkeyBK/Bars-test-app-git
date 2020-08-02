using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bars_test_app
{
    /// <summary>
    /// Статический класс для получения данных с серверов из конфигурации
    /// </summary>
    public static class SqlWorker
    {
        /// <summary>
        /// Метод для получения и оформления данных
        /// </summary>
        /// <returns></returns>
        public static List<List<List<object>>> getData()
        {
            Console.WriteLine($"Началась загрузка {DateTime.Now}");
            //Подобная сложная запись связана с форматом работы api гугла, однако вместо массивов я использую списки,тк динамичность
            List<List<List<object>>> result = new List<List<List<object>>>();

            foreach (var connection in Cache.Settings.DBConnections)
            { 
                List<List<object>> Bases = new List<List<object>>();
                double dbsizesum = 0;

                var conn = new NpgsqlConnection(connection.Connection_string);
                conn.Open();

                //решил оставить запрос в таком виде,тк он показался мне более-менее читаемым,хотя уверен что в кодестайле компании прописан формат записи
                var cmd = new NpgsqlCommand($"select datname, pg_size_pretty(pg_database_size(datname)),(pg_stat_file('base/'||oid ||'/PG_VERSION')).modification from pg_database;", conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Bases.Add(new List<object> { connection.Db_Name, (string)reader[0], ((string)reader[1]).ToGB(),((DateTime)reader[2]).Date });
                    dbsizesum += ((string)reader[1]).ToGB();
                }

                //финальная и первая строчка
                Bases.Add(new List<object> { 
                    connection.Db_Name, 
                    connection.Max_Size > dbsizesum ? "Свободно" : "Превышено", 
                    Math.Abs(connection.Max_Size - dbsizesum), 
                    Bases.Select(x => x.Last()).OrderBy(x => x).First()
                });
                Bases.ForEach(x =>x[3]=((DateTime)x[3]).ToString("dd.mm.yyyy"));
                Bases.Insert(0, new List<object>() { "Сервер", "База данных", "Размер в ГБ", "Дата обновления" });

                conn.Close();
                
                result.Add(Bases);
                Console.WriteLine($"Для сервера \"{connection.Db_Name}\" считано {Bases.Count} строчек данных");
            }
            return result;
        }
    }
}
