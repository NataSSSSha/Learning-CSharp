namespace TelegramBotASPEC
{
    public class BotUpdate
    {
        /// <summary>
        /// 1 сообщение от пользователя.
        /// </summary>
        public string? text;

        /// <summary>
        /// 2 сообщение от пользователя(если есть).
        /// </summary>
        public string? anotherText;

        /// <summary>
        /// ID чата пользователя.
        /// </summary>
        public long id;

        /// <summary>
        /// ID сообщения пользователя.
        /// </summary>
        public long messageId;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="text">Сообщение от пользователя.</param>
        /// <param name="id">ID чата пользователя.</param>
        public BotUpdate(string? text, long id)
        {
            this.text = text;
            this.id = id;
        }
    }
}
