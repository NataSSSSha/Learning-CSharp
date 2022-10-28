using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.Enums;

namespace TelegramBotASPEC
{
    internal class SendMessageAndKeyboard
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// ID чата администратора.
        /// </summary>
        int adminChatID = 1;

        GoogleSheets table = new();

        const string path = "path";

        /// <summary>
        /// Отправить стартовое сообщение.
        /// </summary>
        /// <param name="botClient">Бот-клиент.</param>
        /// <param name="update">Обновление.</param>
        /// <returns></returns>
        public async Task SendStartMessage(ITelegramBotClient botClient, Update update)
        {
            BotUpdateManager.DeleteUpdate(BotUpdateManager.FindUpdateByID(update.Message.Chat.Id));
            await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Привет!\U0001F44B	 Я твой помощник! \n" +
                "Я помогу тебе \n \U0001F4DC заказать нужную справку \n \U0001F4C4 получить образец заявления \n \U0001F4E8 ты можешь задать мне любой вопрос, я постараюсь ответить.\U0001F60A \nИтак,", ParseMode.Html);
            _ = StartKeyboard(botClient, update.Message);
            logger.Info($"{update.Message.Chat.Id} Отправлено стартовое сообщение");
            return;
        }

        /// <summary>
        /// Отправить пользователю информацию о видах справок или формузаявления.
        /// </summary>
        /// <param name="botClient">Бот-клиент.</param>
        /// <param name="update">Обновление.</param>
        /// <returns></returns>
        public async Task SendStatementKeyboard(ITelegramBotClient botClient, Update update)
        {
            BotUpdate botUpdate = new(update.Message.Text, update.Message.Chat.Id);
            BotUpdateManager.AddBotUpdates(botUpdate);

            if (botUpdate.text == Keyboard.arrayStatement[0] || botUpdate.text == Keyboard.arrayStatement[2] || botUpdate.text == Keyboard.arrayStatement[6])
            {
                _ = StatementKeyboard(botClient, update.Message);
                return;
            }

            else
            {
                using (var stream = System.IO.File.OpenRead("path"))
                {
                    InputOnlineFile iof = new InputOnlineFile(stream);
                    iof.FileName = "Заявление.doc";

                    var send = await botClient.SendDocumentAsync(update.Message.Chat.Id, iof, "Текст сообщения под файлом");
                }
                logger.Info($"{update.Message.Chat.Id} Отправлен файл");
                Keyboard.start.ResizeKeyboard = true;
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Нажми старт для продолжения", replyMarkup: Keyboard.start);
                return;
            }
        }

        /// <summary>
        /// Отправить пользователю стартовую клавиатуру.
        /// </summary>
        /// <param name="botClient">Бот-клиент.</param>
        /// <param name="update">Обновление.</param>
        /// <returns></returns>
        public static async Task StartKeyboard(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Чем тебе помочь?", replyMarkup: Keyboard.keyboardFirst);
            return;
        }

        /// <summary>
        /// Отправить администатору заявку и записать в гугл-таблицу.
        /// </summary>
        /// <param name="botClient">Бот-клиент.</param>
        /// <param name="update">Обновление.</param>
        /// <param name="botUpdate">Данные сообщения пользователя.</param>
        /// <param name="adminId">ID чата администратора.</param>
        /// <returns></returns>
        async Task ReplyKeyboard(ITelegramBotClient botClient, BotUpdate botUpdate, Update update, int adminId)
        {
            string text = $"Заказ {botUpdate.text} {botUpdate.anotherText} на имя {update.Message.Text} данные чата: {update.Message.Chat.FirstName} " +
                          $"{update.Message.Chat.LastName} {update.Message.Chat.Username}";
            Message message = await botClient.SendTextMessageAsync(adminId, text, replyMarkup: Keyboard.keyboardToAdmin);
            logger.Info($"{adminChatID} Отправлен запрос администратору на {update.Message.Text} от {update.Message.Chat.Id}");
            await table.CreateEntry(update.Message.Date.ToString(), botUpdate.text + botUpdate.anotherText, update.Message.Text, update.Message.Chat.FirstName +
                                    update.Message.Chat.LastName, message.MessageId);
            ReqManager.AddReq(text, update.Message.Chat.Id, message.MessageId);
            return;
        }

        /// <summary>
        /// Отправить пользователю клавиатру с выбором справок.
        /// </summary>
        /// <param name="botClient">Бот-клиент.</param>
        /// <param name="callbackQuery">Обновление.</param>
        /// <returns></returns>
        public static async Task OrderKeyboard(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            Keyboard.keyboardOrder.OneTimeKeyboard = true;
            await botClient.EditMessageReplyMarkupAsync(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId, null);
            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Ты выбрал {callbackQuery.Data}");
            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Хорошо, какая именно справка или копия нужна:", replyMarkup: Keyboard.keyboardOrder);
            return;
        }

        /// <summary>
        /// Отправить пользователю варианты форм заявлений.
        /// </summary>
        /// <param name="botClient">Бот-клиент.</param>
        /// <param name="callbackQuery">Обратный вызов.</param>
        /// <returns></returns>
        public static async Task StateKeyboard(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            Keyboard.keyboardState.OneTimeKeyboard = true;
            await botClient.EditMessageReplyMarkupAsync(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId, null);
            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Какая форма нужна:", replyMarkup: Keyboard.keyboardState);
            logger.Info($"{callbackQuery.Message.Chat.Id} Отправлен файл");
            return;
        }

        /// <summary>
        /// Отправляет пользователю информацию о вариантах заявлений на отпуск, мат.помощь, перечислении зп или трудовой книжки.
        /// </summary>
        /// <param name="botClient">Бот-клиент.</param>
        /// <param name="message">Сообщение пользователя.</param>
        /// <returns></returns>
        static async Task StatementKeyboard(ITelegramBotClient botClient, Message message)
        {

            if (message.Text.Contains("На отпуск."))
            {
                Keyboard.vacationKeyboard.OneTimeKeyboard = true;
                await botClient.SendTextMessageAsync(message.Chat.Id, "Выбери вид отпуска", replyMarkup: Keyboard.vacationKeyboard);
                logger.Info($"{message.Chat.Id} Отправлены виды отпуска");
                return;
            }

            if (message.Text.Contains("На мат. помощь."))
            {
                Keyboard.helpKeyboard.OneTimeKeyboard = true;
                await botClient.SendTextMessageAsync(message.Chat.Id, "Укажи причину", replyMarkup: Keyboard.helpKeyboard);
                logger.Info($"{message.Chat.Id} Отправлены причины получения мат.помощи");
                return;
            }

            if (message.Text.Contains("О перечислении ЗП на карту."))
            {
                Keyboard.checkingAccountKeyboard.OneTimeKeyboard = true;
                await botClient.SendTextMessageAsync(message.Chat.Id, "Выбери вид заявления:", replyMarkup: Keyboard.checkingAccountKeyboard);
                logger.Info($"{message.Chat.Id} Отправлены виды заявления о перечислении зп");
                return;
            }
        }

        /// <summary>
        /// Отправить пользователю информацию о справке о зп или трудовой книжке.
        /// </summary>
        /// <param name="botClient">Бот-клиент.</param>
        /// <param name="message">Сообщение пользователя.</param>
        /// <returns></returns>
        public static async Task CertificateKeyboard(ITelegramBotClient botClient, Message message)
        {
            if (message.Text.Contains("Cправка о среднем заработкe."))
            {
                Keyboard.mediumSalaryKeyboard.OneTimeKeyboard = true;
                await botClient.SendTextMessageAsync(message.Chat.Id, "Ок, за какой период нужна справка:", replyMarkup: Keyboard.mediumSalaryKeyboard);
                logger.Info($"{message.Chat.Id} Отправлены периоды справки о зп.");
                return;
            }

            if (message.Text.Contains("Трудовая книжка."))
            {
                Keyboard.employmentHistoryKeyboard.OneTimeKeyboard = true;
                await botClient.SendTextMessageAsync(message.Chat.Id, "Ок, Копия или оригинал?", replyMarkup: Keyboard.employmentHistoryKeyboard);
                logger.Info($"{message.Chat.Id} Отправлены типы трудовой");
                return;
            }
        }

        /// <summary>
        /// Ответить пользователю на запрос о трудовой книжке или или зп или запросить ФИО или запрос
        /// </summary>
        /// <param name="botClient">Бот-клиент.</param>
        /// <param name="update">Обновление.</param>
        /// <returns></returns>
        public async Task SendMessageIfArrayCertificate(ITelegramBotClient botClient, Update update)
        {
            BotUpdate botUpdate = new(update.Message.Text, update.Message.Chat.Id);
            BotUpdateManager.AddBotUpdates(botUpdate);

            if (update.Message.Text == Keyboard.arrayCertificate[1] || update.Message.Text == Keyboard.arrayCertificate[4])
            {
                _ = CertificateKeyboard(botClient, update.Message);
                return;
            }

            if (update.Message.Text == Keyboard.arrayCertificate[5])
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Хорошо, напиши свой запрос");
                logger.Info($"{update.Message.Chat.Id} Отправлен запрос на необходимую справку");
                return;
            }

            else
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Отлично, напиши свое ФИО (без сокращений)");
                logger.Info($"{update.Message.Chat.Id} Отправлен запрос на ФИО");
                return;
            }
        }

        /// <summary>
        /// Отправить пользователю ответное сообщение.
        /// </summary>
        /// <param name="botClient">Бот-клиент.</param>
        /// <param name="update">Обновление.</param>
        /// <returns></returns>
        public async Task SendMessageSecondPart(ITelegramBotClient botClient, Update update)
        {
            BotUpdate botUpdate = BotUpdateManager.FindUpdateByID(update.Message.Chat.Id);

            if (Keyboard.arrayMediumSalary.Contains(update.Message.Text) || Keyboard.arrayEmploymentHistory.Contains(update.Message.Text))
            {
                botUpdate.text += update.Message.Text;
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Отлично, напиши свое ФИО (без сокращений)");
                logger.Info($"{update.Message.Chat.Id} Отправлен запрос на ФИО");
                return;
            }

            if (botUpdate.text == Keyboard.arrayCertificate[5] && botUpdate.anotherText == null || 
                botUpdate.text == Keyboard.arrayStart[4] && botUpdate.anotherText == null)
            {
                botUpdate.anotherText = update.Message.Text;
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Отлично, напиши свое ФИО (без сокращений)");
                logger.Info($"{update.Message.Chat.Id} Отправлен запрос на ФИО");
                return;
            }

            if (botUpdate.anotherText != null)
            {
                Console.WriteLine($"Заказ {botUpdate.anotherText} на имя {update.Message.Text} чат {update.Message.Chat.Id}" +
                                  $"{ update.Message.Chat.LastName} { update.Message.Chat.Username}");
                _ = ReplyKeyboard(botClient, botUpdate, update, adminChatID);
                BotUpdateManager.DeleteUpdate(BotUpdateManager.FindUpdateByID(update.Message.Chat.Id));
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Ок, разберемся, сообщим");
                Keyboard.start.ResizeKeyboard = true;
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Нажми старт для продолжения", replyMarkup: Keyboard.start);
                return;
            }

            if (botUpdate.text == Keyboard.arrayCertificate[4] + Keyboard.arrayEmploymentHistory[1])
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "За оригиналом трудовой книжки нужно подойти в отдел управления персоналом по адресу: " +
                                                    "ул.Пушкинская, 268");
                logger.Info($"{update.Message.Chat.Id} Отправлена информация о получении оригинала трудовой");
                BotUpdateManager.DeleteUpdate(BotUpdateManager.FindUpdateByID(update.Message.Chat.Id));
                Keyboard.start.ResizeKeyboard = true;
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Нажми старт для продолжения", replyMarkup: Keyboard.start);
                return;
            }

            if (botUpdate.text == Keyboard.arrayCertificate[0] || botUpdate.text == Keyboard.arrayCertificate[2] ||
                botUpdate.text.StartsWith(Keyboard.arrayCertificate[1] + "за")|| 
                botUpdate.text == Keyboard.arrayCertificate[3] || botUpdate.text.StartsWith(Keyboard.arrayCertificate[4] + Keyboard.arrayEmploymentHistory[0]))
            {
                Console.WriteLine($"Заказ {botUpdate.text} на имя {update.Message.Text} чат {update.Message.Chat.Id}");
                _ = ReplyKeyboard(botClient, botUpdate, update, adminChatID);
                BotUpdateManager.DeleteUpdate(BotUpdateManager.FindUpdateByID(update.Message.Chat.Id));
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "В течение 2-х дней будет готово! Сообщим, как сделаем!");
                Keyboard.start.ResizeKeyboard = true;
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Нажми старт для продолжения", replyMarkup: Keyboard.start);
                return;
            }

            if (Keyboard.arrayVocation.Contains(update.Message.Text))
            {
                using (var stream = System.IO.File.OpenRead("path"))
                {
                    InputOnlineFile iof = new InputOnlineFile(stream);
                    iof.FileName = "Заявление.doc";

                    var send = await botClient.SendDocumentAsync(update.Message.Chat.Id, iof, "Текст сообщения под файлом");
                }
                Keyboard.start.ResizeKeyboard = true;
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Нажми старт для продолжения", replyMarkup: Keyboard.start);
                logger.Info($"{update.Message.Chat.Id} Отправлен файл");
                return;
            }

            if (Keyboard.arrayCheckingAccount.Contains(update.Message.Text))
            {
                using (var stream = System.IO.File.OpenRead("path"))
                {
                    InputOnlineFile iof = new InputOnlineFile(stream);
                    iof.FileName = "Заявление.doc";

                    var send = await botClient.SendDocumentAsync(update.Message.Chat.Id, iof, "Текст сообщения под файлом");
                }
                Keyboard.start.ResizeKeyboard = true;
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Нажми старт для продолжения", replyMarkup: Keyboard.start);
                logger.Info($"{update.Message.Chat.Id} Отправлен файл");
                return;
            }

            if (Keyboard.arrayHelp.Contains(update.Message.Text))
            {
                using (var stream = System.IO.File.OpenRead("path"))
                {
                    InputOnlineFile iof = new InputOnlineFile(stream);
                    iof.FileName = "Заявление.doc";

                    var send = await botClient.SendDocumentAsync(update.Message.Chat.Id, iof, "Текст сообщения под файлом");
                }
                Keyboard.start.ResizeKeyboard = true;
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Нажми старт для продолжения", replyMarkup: Keyboard.start);
                logger.Info($"{update.Message.Chat.Id} Отправлен файл");
                return;
            }

            else
            {
                if(botUpdate!= null)
                    BotUpdateManager.DeleteUpdate(botUpdate);
                if (Keyboard.arrayCertificate.Contains(update.Message.Text))
                {
                    BotUpdate newbotUpdate = new(update.Message.Text, update.Message.Chat.Id);
                    BotUpdateManager.AddBotUpdates(newbotUpdate);
                    _ = SendMessageIfArrayCertificate(botClient, update);
                    return;
                }
                if (Keyboard.arrayStatement.Contains(update.Message.Text))
                {
                    BotUpdate newbotUpdate = new(update.Message.Text, update.Message.Chat.Id);
                    BotUpdateManager.AddBotUpdates(newbotUpdate);
                    _ = SendStatementKeyboard(botClient, update);
                    return;
                }

                else
                {
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Не совсем тебя понял, давай еще раз");
                    _ = StartKeyboard(botClient, update.Message);
                    return;
                }

            }
        }

        /// <summary>
        /// Отправляет пользователю сообщение о решении вопроса или готовности справки.
        /// </summary>
        /// <param name="botClient">Бот-клиент.</param>
        /// <param name="callbackQuery">Обратный вызов.</param>
        /// <returns></returns>
        public async Task SendAndDeleteMessage(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            if (ReqManager.FindReqByID(callbackQuery.Message.MessageId) == null)
                return;

            long? chatID = ReqManager.FindReqByID(callbackQuery.Message.MessageId)?.chatId;
            string? text = ReqManager.FindReqByID(callbackQuery.Message.MessageId)?.text;
            string? messageid = ReqManager.FindReqByID(callbackQuery.Message.MessageId)?.messageID.ToString();

            if (callbackQuery.Data == "Готово! \U00002705")
            {
                if (text.StartsWith("Заказ \U00002753 Задать") || text.StartsWith("Заказ Другое"))
                {
                    await botClient.SendTextMessageAsync(chatID, $"{text} решен.");
                    logger.Info($"{callbackQuery.Message.Chat.Id} Вопрос решен.");
                }
                else
                {
                    await botClient.SendTextMessageAsync(chatID, $"Ваш {text} готов. За готовым документом нужно подойти в офис по адресу: " +
                                                         $"ул. Пушкинская, 268, 3 этаж, 1 кабинет. Подробности по тел. 908-413");
                    logger.Info($"{callbackQuery.Message.Chat.Id} Справка готова");
                }
            }

            if (callbackQuery.Data == "Отклонить \U0001F6AB")
            {
                await botClient.SendTextMessageAsync(chatID, $"{text} был отклонен. Подробности по тел. 908-413");
                logger.Info($"{callbackQuery.Message.Chat.Id} Запрос отклонен.");
            }

            await table.FindID(messageid, callbackQuery.Data);
            await botClient.DeleteMessageAsync(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId);
            return;
        }
        /// <summary>
        /// Отправить запрос на получение вопроса пользователя.
        /// </summary>
        /// <param name="botClient">Бот-клиент.</param>
        /// <param name="callbackQuery">Обратный вызов.</param>
        /// <returns></returns>
        public async Task Question(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            BotUpdate botUpdate = new(callbackQuery.Data, callbackQuery.Message.Chat.Id);
            BotUpdateManager.AddBotUpdates(botUpdate);

            await botClient.EditMessageReplyMarkupAsync(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId, null);
            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Ты выбрал {callbackQuery.Data}");
            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Напиши свой вопрос");
            logger.Info($"{callbackQuery.Message.Chat.Id} Отправлен запрос на вопрос.");
            return;
        }
    }
}
