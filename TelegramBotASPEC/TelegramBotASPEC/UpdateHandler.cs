using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBotASPEC
{
    internal class UpdateHandler : IUpdateHandler
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        SendMessageAndKeyboard keyboard = new();

        /// <summary>
        /// Обработать сообщение.
        /// </summary>
        /// <param name="botClient">Бот-клиент.</param>
        /// <param name="update">Обновление.</param>
        /// <returns></returns>
        async Task HandleMessage(ITelegramBotClient botClient, Update update)
        {
            logger.Info($"{update.Message.Chat.Id} Получено сообщение {update.Message.Text}");

            if (update.Message.Text == "/start" || update.Message.Text.ToLower() == "start" || update.Message.Text.ToLower() == "старт")
            {
                _ = keyboard.SendStartMessage(botClient, update);
                return;
            }

            if(ReqManager.FindReqByID(update.Message.ReplyToMessage?.MessageId) != null)
            {
                await botClient.SendTextMessageAsync(ReqManager.FindReqByID(update.Message.ReplyToMessage?.MessageId).chatId, update.Message?.Text);
                return;
            }

            //1.для второго сообщения(1 часть)
            if(Keyboard.arrayCertificate.Contains(update.Message.Text))
            {
                _ = keyboard.SendMessageIfArrayCertificate(botClient, update);
                return;
            }

            //2.для второго сообщения(1 часть)
            if (Keyboard.arrayStatement.Contains(update.Message.Text))
            {
                _ = keyboard.SendStatementKeyboard(botClient, update);
                return;
            }

            //2.для второго сообщения(2 часть)
            if (BotUpdateManager.FindUpdateByID(update.Message.Chat.Id) != null)
            {
                _ = keyboard.SendMessageSecondPart(botClient, update);
                return;
            }

            return;
        }
        /// <summary>
        /// Обработать нажатие кнопки(Inline-клавиатура).
        /// </summary>
        /// <param name="botClient">Бот-клиент.</param>
        /// <param name="callbackQuery">Обратный вызов.</param>
        /// <param name="cancellationToken">Увдомление об отмене.</param>
        /// <returns></returns>
        async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            logger.Info($"{callbackQuery.Message.Chat.Id} Получено сообщение {callbackQuery.Data} (кнопка)");

            if (Keyboard.arrayStart.Contains(callbackQuery.Data))
            {
                if (callbackQuery.Data == Keyboard.arrayStart[0])
                    _ = SendMessageAndKeyboard.OrderKeyboard(botClient, callbackQuery);

                if (callbackQuery.Data == Keyboard.arrayStart[1])
                    _ = SendMessageAndKeyboard.StateKeyboard(botClient, callbackQuery);

                if(callbackQuery.Data == Keyboard.arrayStart[4])
                {
                    _ = keyboard.Question(botClient, callbackQuery);
                }
            }

            if(callbackQuery.Data == "Готово! \U00002705" || callbackQuery.Data == "Отклонить \U0001F6AB")
            {
                _ = keyboard.SendAndDeleteMessage(botClient, callbackQuery);
            }

            return;
        }

        /// <summary>
        /// Обработать асинхронно сообщение.
        /// </summary>
        /// <param name="botClient">Бот-клиент.</param>
        /// <param name="update">Обновление.</param>
        /// <param name="cancellationToken">Увдомление об отмене.</param>
        /// <returns></returns>
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandleMessage(botClient, update);
                return;
            }

            if (update.Type == UpdateType.CallbackQuery)
            {
                await HandleCallbackQuery(botClient, update.CallbackQuery, cancellationToken);
                return;
            }
        }

        /// <summary>
        /// Обработать ошибки.
        /// </summary>
        /// <param name="botClient">Бот-клиент.</param>
        /// <param name="exception">Исключение.</param>
        /// <param name="cancellationToken">Увдомление об отмене.</param>
        /// <returns></returns>
        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.Error.WriteLine(exception);
            return Task.CompletedTask;
        }
    }
}

