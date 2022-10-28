namespace TelegramBotASPEC
{
    public static class BotUpdateManager
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Список с данными сообщений пользователей.
        /// </summary>
        public static List<BotUpdate> botUpdates = new List<BotUpdate>();

        /// <summary>
        /// Добавить данные в список.
        /// </summary>
        /// <param name="text">Текст сообщения.</param>
        /// <param name="id">ID чата.</param>
        public static void AddBotUpdates(string? text, long id)
        {
            botUpdates.Add(new BotUpdate(text, id));
            logger.Info($"{id} Добавлены данные о сообщении.");
        }

        /// <summary>
        /// Добавить данные в список.
        /// </summary>
        /// <param name="botUpdate">Данные сообщения.</param>
        public static void AddBotUpdates(BotUpdate botUpdate)
        {
            botUpdates.Add(botUpdate);
            logger.Info($"{botUpdate.id} Добавлены данные о сообщении.");
        }

        /// <summary>
        /// Найти данные сообщения по ID чата.
        /// </summary>
        /// <param name="id">ID чата.</param>
        /// <returns>Данные сообщения с требуемым ID чата.</returns>
        public static BotUpdate FindUpdateByID(long id) => botUpdates.Find(item => item.id == id);

        /// <summary>
        /// Удалить данные сообщения.
        /// </summary>
        /// <param name="botUpdate">Данные сообщения.</param>
        public static void DeleteUpdate(BotUpdate botUpdate)
        {
            logger.Info($"{botUpdate?.id} Данные о сообщении удалены.");
            botUpdates.Remove(botUpdate);
        }
    }
}
