using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Bars_test_app
{
    /// <summary>
    /// Класс для связи с API Google Sheets
    /// </summary>
    public static class GoogleTableWorker
    {
        /// <summary>
        /// Условно говоря разрешения(доступ) которое требует приложение
        /// </summary>
        static string[] Scopes = { 
            
            SheetsService.Scope.Spreadsheets,
        };

        /// <summary>
        /// Метод для отправки данных баз знаний
        /// </summary>
        /// <param name="Databases"></param>
        /// <returns></returns>
        public static bool SetDataToTable(List<List<List<object>>> Databases)
        {
            //авторизация 
            UserCredential credential;

            string credPath = "token.json";
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets()
            {
                ClientId = Cache.Settings.GoogleConnection.ClientId,
                ClientSecret = Cache.Settings.GoogleConnection.ClientSecret
            },
            Scopes,
            "user",
            CancellationToken.None,
            new FileDataStore(credPath, true)).Result;
            Console.WriteLine("Credential file saved to: " + credPath);

            //запускаем сервис для отправки реквестов на API
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName ="db-table",
            });

            //собираем реквест для обновления данных
            BatchUpdateValuesRequest requestBody = new BatchUpdateValuesRequest();
            requestBody.ValueInputOption = "RAW";

            List<string> serverNames = new List<string>();

            List<ValueRange> valueRanges = new List<ValueRange>();
            foreach (var db in Databases)
            {
                string serverName = (string)db.Select(x => x.Select(y => y).First()).Last();
                serverNames.Add(serverName);
                ValueRange data = new ValueRange()
                {
                    Values = db.Select(x => x.Select(y => y).ToArray()).ToArray(),
                    Range = $"{serverName}!A1:D{db.Count}"
                };
                valueRanges.Add(data);
            }

            //получаем все листы в таблице
            var SpreadsheetRequest = service.Spreadsheets.Get(Cache.Settings.GoogleConnection.SheetID);
            var SheetsInSpreadsheet = SpreadsheetRequest.Execute().Sheets
                .Select(x => x.Properties)
                .ToDictionary(x => x.Title, y => y.SheetId);

            //создание новых листов
            BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest();
            batchUpdateSpreadsheetRequest.Requests = new List<Request>();
            foreach (var name in serverNames)
            {
                if (SheetsInSpreadsheet.Keys.Where(x=>x==name).ToList().Count!=0) continue;
                batchUpdateSpreadsheetRequest.Requests = new List<Request>();
                var addSheetRequest = new AddSheetRequest();
                addSheetRequest.Properties = new SheetProperties();
                addSheetRequest.Properties.Title = name;
                batchUpdateSpreadsheetRequest.Requests.Add(new Request
                {
                    AddSheet = addSheetRequest
                });
            }

            //удаление неактуальных листов
            foreach (var sheet in SheetsInSpreadsheet)
            {
                if (serverNames.Contains(sheet.Key)) continue;
                var deleteSheetRequest = new DeleteSheetRequest();
                deleteSheetRequest.SheetId = sheet.Value;
                batchUpdateSpreadsheetRequest.Requests.Add(new Request
                {
                    DeleteSheet = deleteSheetRequest
                });
            }

            //если ничего в плане листов не поменялось то ничего и не шлём
            if (batchUpdateSpreadsheetRequest.Requests.Count != 0)
            {
                var batchUpdateRequest =
                                    service.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, Cache.Settings.GoogleConnection.SheetID);
                batchUpdateRequest.Execute();
            }
            

            requestBody.Data = valueRanges.ToArray();

            var request = service.Spreadsheets.Values.BatchUpdate(requestBody, Cache.Settings.GoogleConnection.SheetID);

            var response = request.Execute();

            Console.WriteLine($"Таблица обновлена {DateTime.Now} \nCледующая проверка {DateTime.Now.Add(Cache.Settings.TimeOffset.ToTimespan())}");
            return true;
        }

    }
}
