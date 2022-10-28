using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace TelegramBotASPEC
{
    internal class GoogleSheets
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Массив для доступа к электронным таблицам.
        /// </summary>
        static readonly string[] scopes = { SheetsService.Scope.Spreadsheets };

        /// <summary>
        /// Имя приложения.
        /// </summary>
        static readonly string applicationName = "ApplicationName";

        /// <summary>
        /// ID гугл-таблицы.
        /// </summary>
        static readonly string spreadSheetID = "1ONL1kRW1v8D6M6uWAd3IdeQf_2Bd5abdQ6o2EhDuPIo";

        /// <summary>
        /// Название листа в гугл-таблице.
        /// </summary>
        static readonly string sheet = "Requests";

        /// <summary>
        /// 
        /// </summary>
        static SheetsService service;

        /// <summary>
        /// Учетные данные для работы с гугл-таблицей.
        /// </summary>
        GoogleCredential credential;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public GoogleSheets()
        {
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(scopes);
            }
            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName,
            });
        }

        /// <summary>
        /// Создает новую строку запроса в гугл-таблице.
        /// </summary>
        /// <param name="date">Дата и время.</param>
        /// <param name="text">Текст запроса.</param>
        /// <param name="name">ФИО пользователя.</param>
        /// <param name="chatData">Данные о чате.</param>
        /// <param name="messID">ID сообщения.</param>
        /// <returns></returns>
        public async Task CreateEntry(string date, string text, string name, string chatData, int messID)
        {
            var range = $"{sheet}!A:E";
            var valueRange = new ValueRange();

            var objectList = new List<object>() { messID, date, text, name, chatData };

            valueRange.Values = new List<IList<object>> { objectList };

            var appendRequest = service.Spreadsheets.Values.Append(valueRange, spreadSheetID, range);

            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

            var appendResponse = appendRequest.Execute();

            logger.Info($"{messID} Добавлен запрос в гугл-таблицу.");
        }

        /// <summary>
        /// Находит по ID нужный запрос в гугл-таблице.
        /// </summary>
        /// <param name="messid">ID сообщения.</param>
        /// <param name="state">Статус запроса.</param>
        /// <returns></returns>
        public async Task FindID(string messid, string state)
        {
            var range = $"{sheet}!A:E";

            var request = service.Spreadsheets.Values.Get(spreadSheetID, range);

            var response = request.Execute();

            var values = response.Values;

            int numberOfLine = 0;

            foreach(var row in values)
            {
                numberOfLine++;
                if (row[0].ToString() == messid)
                {
                    await Task.Run(() => AddState(numberOfLine, state, messid));
                    logger.Info($"{messid} Найден в гугл-таблице.");
                }
            }
        }

        /// <summary>
        /// Устанавливает статус запроса в гугл-таблице.
        /// </summary>
        /// <param name="number">Номер строки.</param>
        /// <param name="state">Статус.</param>
        void AddState(int number, string state, string messid)
        {
            var range = $"{sheet}!F{number}";
            var valueRange = new ValueRange();

            var objectList = new List<object>() { {state} };

            valueRange.Values = new List<IList<object>> { objectList };

            var appendRequest = service.Spreadsheets.Values.Append(valueRange, spreadSheetID, range);

            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

            var appendResponse = appendRequest.Execute();

            logger.Info($"{messid} Добавлен статус для запроса");
        }

        
    }
}
