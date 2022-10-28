namespace TelegramBotASPEC
{
    public class Req
    {
        /// <summary>
        /// ID сообщения.
        /// </summary>
        public int? messageID;

        /// <summary>
        /// Текст сообщения.
        /// </summary>
        public string text;

        /// <summary>
        /// ID чата.
        /// </summary>
        public long chatId;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="text">Текст сообщения.</param>
        /// <param name="chatId">ID чата.</param>
        /// <param name="messageID">ID сообщения.</param>
        public Req(string text, long chatId, int messageID)
        {
            this.text = text;
            this.chatId = chatId;
            this.messageID = messageID;
        }
    }
}
