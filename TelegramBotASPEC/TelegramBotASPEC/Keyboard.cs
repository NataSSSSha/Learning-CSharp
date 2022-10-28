using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotASPEC
{
    internal class Keyboard
    {
        /// <summary>
        /// Массив возможных ответов пользователя при начале работы.
        /// </summary>
        public static string[] arrayStart = {"\U0001F6D2 Заказать справку/копию", "\U00002B07 Получить форму заявления", "Отправить заявление",
                          "Изменились данные", "\U00002753 Задать вопрос"};

        /// <summary>
        /// Массив возможных ответов пользователя при выборе заказа справки.
        /// </summary>
        public static string[] arrayCertificate = {"Cправка 2-НДФЛ.", "Cправка о среднем заработкe.", "Справка о неполучении пособия вторым родителем.",
                          "Справка с места работы.", "Трудовая книжка.", "Другое."};

        /// <summary>
        /// Массив возможных ответов пользователя при выборе среднего заработка.
        /// </summary>
        public static string[] arrayMediumSalary = { "за 3 месяца", "за полгода", "за год" };

        /// <summary>
        /// Массив возможных ответов пользователя при выборе трудовой книжки.
        /// </summary>
        public static string[] arrayEmploymentHistory = { "Копия", "Оригинал" };

        /// <summary>
        /// Массив возможноых ответов пользователя при выборе формы заявления.
        /// </summary>
        public static string[] arrayStatement = {"На отпуск.","О переводе.", "О перечислении ЗП на карту.", "Об увольнении.", "На переход на эл.трудовую книжку.",
                                          "Служебная записка.", "На мат. помощь."};

        /// <summary>
        /// Массив возможных ответов пользователя при выборе заявления на отпуск.
        /// </summary>
        public static string[] arrayVocation = { "Ежегодный оплачиваемый", "Без сохранения ЗП", "Ученический" };

        /// <summary>
        /// Массив возможных ответов пользователя при выборе заявления на перечисление зп.
        /// </summary>
        public static string[] arrayCheckingAccount = { "На свою карту", "На карту другого человека" };

        /// <summary>
        /// Массив возможных ответов пользователя при выборе заявления на мат.помощь.
        /// </summary>
        public static string[] arrayHelp = { "смерть близкого родственника", "рождение ребенка", "заключение брака впервые" };

        /// <summary>
        /// Клавиатура для старта.
        /// </summary>
        public static InlineKeyboardMarkup keyboardFirst = new(new[]
        {
            new[]
            {
                 InlineKeyboardButton.WithCallbackData ("\U0001F6D2 Заказать справку/копию"),
            },
            new[]
            {
                 InlineKeyboardButton.WithCallbackData ("\U00002B07 Получить форму заявления"),
            },
                    /*new[]
                     {
                        InlineKeyboardButton.WithCallbackData ("Отправить заявление"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData ("Изменились данные"),
                    },*/
            new[]
            {
                 InlineKeyboardButton.WithCallbackData ("\U00002753 Задать вопрос"),
            },
        });

        /// <summary>
        /// Клавиатура для администратора.
        /// </summary>
        public static InlineKeyboardMarkup keyboardToAdmin = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData ("Готово! \U00002705"),
                InlineKeyboardButton.WithCallbackData ("Отклонить \U0001F6AB"),
            },
        });

        /// <summary>
        /// Клавиатура для выбора справки.
        /// </summary>
        public static ReplyKeyboardMarkup keyboardOrder = new(new[]
        {
            new KeyboardButton[] {"Cправка 2-НДФЛ."},
            new KeyboardButton[] {"Cправка о среднем заработкe."},
            new KeyboardButton[] {"Справка о неполучении пособия вторым родителем."},
            new KeyboardButton[] {"Справка с места работы."},
            new KeyboardButton[] {"Трудовая книжка."},
            new KeyboardButton[] {"Другое."},
        });

        /// <summary>
        /// Клавиатура для выбора заявления.
        /// </summary>
        public static ReplyKeyboardMarkup keyboardState = new(new[]
        {
            new KeyboardButton[] {"На отпуск."},
            new KeyboardButton[] {"О переводе."},
            new KeyboardButton[] {"О перечислении ЗП на карту."},
            new KeyboardButton[] {"Об увольнении."},
            new KeyboardButton[] {"На переход на эл.трудовую книжку."},
            new KeyboardButton[] {"Служебная записка."},
            new KeyboardButton[] {"На мат. помощь."},
        });

        /// <summary>
        /// Клавиатура для выбора вида заявления на отпуск.
        /// </summary>
        public static ReplyKeyboardMarkup vacationKeyboard = new(new[]
        {
            new KeyboardButton[] {"Ежегодный оплачиваемый"},
            new KeyboardButton[] {"Без сохранения ЗП"},
            new KeyboardButton[] {"Ученический"},
        });

        /// <summary>
        /// Клавиатура для выбора вида заявления на расчетный счет.
        /// </summary>
        public static ReplyKeyboardMarkup checkingAccountKeyboard = new(new[]
        {
            new KeyboardButton[] {"На свою карту"},
            new KeyboardButton[] {"На карту другого человека"},
        });

        /// <summary>
        /// Клавиатура для выбора заявления на мат.помощь.
        /// </summary>
        public static ReplyKeyboardMarkup helpKeyboard = new(new[]
        {
            new KeyboardButton[] {"смерть близкого родственника"},
            new KeyboardButton[] {"рождение ребенка"},
            new KeyboardButton[] {"заключение брака впервые"},
        });

        /// <summary>
        /// Клавиатура для выбора справки о з/п.
        /// </summary>
        public static ReplyKeyboardMarkup mediumSalaryKeyboard = new(new[]
        {
            new KeyboardButton[] {"за 3 месяца"},
            new KeyboardButton[] {"за полгода"},
            new KeyboardButton[] {"за год"},
        });

        /// <summary>
        /// Клавиатура для выбора вида трудовой книжки.
        /// </summary>
        public static ReplyKeyboardMarkup employmentHistoryKeyboard = new(new[]
        {
            new KeyboardButton[] {"Копия"},
            new KeyboardButton[] {"Оригинал"},
        });

        /// <summary>
        /// Кнапка для старта.
        /// </summary>
        public static ReplyKeyboardMarkup start = new(new[] { new KeyboardButton[] { "Старт" } });

        
        
    }
}
