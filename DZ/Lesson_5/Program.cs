namespace Lesson_5
{
    internal static class Program 
    {
        /// <summary>
        /// Вывести меню.
        /// </summary>
        public static void ShowMenu()
        {
            Console.WriteLine("Выбери действие:\n" +
                              "1 - добавить контакт\n" +
                              "2 - удалить контакт\n" +
                              "3 - найти контакт по имени\n" +
                              "4 - найти контакт по номеру\n" +
                              "5 - завершить программу");
        }

        /// <summary>
        /// Вывести текст зеленым цветом
        /// </summary>
        /// <param name="message">Текст</param>
        public static void DisplayGreenMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Вывести текст красным цветом
        /// </summary>
        /// <param name="message">Текст</param>
        public static void DisplayRedMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void Main()
        {
            var phoneBook = PhoneBook.GetIntance();
            
            phoneBook.NotifyMessage += message => Console.WriteLine(message);
            phoneBook.NotifyMessageRemove += DisplayRedMessage;
            phoneBook.NotifyMessageAdd += DisplayGreenMessage;

            phoneBook.LoadSubscrivers();

            int numberOfMenu;
            do
            {
                ShowMenu();

                bool result = int.TryParse(Console.ReadLine(), out numberOfMenu);
                if (result)
                    switch (numberOfMenu)
                    {
                        case 1:
                        {
                            Console.WriteLine("Введите имя:");
                            string? newName = Console.ReadLine();
                            Console.WriteLine("Введите номер:");
                            string? newPhoneNumber = Console.ReadLine();

                            phoneBook.AddSubscriber(newName, newPhoneNumber);
                        }
                            break;
                        case 2:
                        {
                            if (phoneBook.subscribers.Count == 0)
                                Console.WriteLine("Пока нет ни одного абонента:(");
                            else
                            {
                                Console.WriteLine("Введите имя абонента, которого хотите удалить");
                                string? name = Console.ReadLine();
                                    phoneBook.DeleteSybscriber(name);
                            }
                        }
                            break;
                        case 3:
                        {
                            Console.WriteLine("Введите имя абонента:");
                            string? name = Console.ReadLine();
                            try
                            {
                                Console.WriteLine(phoneBook.GetSubscriberByName(name).PhoneNumber);
                            }
                            catch (NullReferenceException)
                            {
                                Console.WriteLine("Абонент не найден");    
                            }
                        }
                            break;
                        case 4:
                        {
                            Console.WriteLine("Введите номер абонента:");
                            string? phoneNumber = Console.ReadLine();
                            try
                            {
                                Console.WriteLine(phoneBook.GetSubscriberByNumber(phoneNumber).Name);
                            }
                            catch (NullReferenceException)
                            {
                                Console.WriteLine("Абонент не найден");
                            }
                        }
                            break;
                        case 5:
                            phoneBook.WriteToFile();
                            break;
                        default:
                            Console.WriteLine("Неверный номер");
                            break;
                    }
                else
                    Console.WriteLine("Неверное значение");
            }
            while (numberOfMenu != 5);
        }
    }
}