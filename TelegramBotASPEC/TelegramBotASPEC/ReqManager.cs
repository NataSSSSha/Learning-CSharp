namespace TelegramBotASPEC
{
    public static class ReqManager
    {
        /// <summary>
        /// Список с запросами пользователей.
        /// </summary>
        public static List<Req> reqs = new List<Req>();

        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Добавить запрос.
        /// </summary>
        /// <param name="text">Текст запроса.</param>
        /// <param name="chatId">ID чата.</param>
        /// <param name="messageId">ID сообщения.</param>
        public static void AddReq(string text, long chatId, int messageId)
        {
            reqs.Add(new Req(text, chatId, messageId));
            logger.Info($"{chatId} Добавлен запрос.");
        }

        /// <summary>
        /// Найти запрос по ID сообщения.
        /// </summary>
        /// <param name="messageId">ID сообщения.</param>
        /// <returns>Запрос с требуемым ID</returns>
        public static Req FindReqByID(int? messageId) => reqs.Find(item => item.messageID == messageId);


        /// <summary>
        /// Найти запрос по тексту сообщения.
        /// </summary>
        /// <param name="text">Текст сообщения.</param>
        /// <returns>Запрос с требуемым текстом.</returns>
        public static Req FindReqByText(string text) => reqs.Find(item => item.text == text);
    }
}
