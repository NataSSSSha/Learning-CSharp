using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{
    /// <summary>
    /// Справочник для работы с абонентами.
    /// </summary>
    public class PhoneBook
    {
        /// <summary>
        /// Экземпляр класса.
        /// </summary>
        private static PhoneBook intance;

        /// <summary>
        /// Имя файла, в котором хранятся данные об абонентах.
        /// </summary>
        private const string nameOfFile = "phonebook.txt";

        /// <summary>
        /// Путь к файлу с данными абонентов.
        /// </summary>
        private string filePath = Path.Combine(Directory.GetCurrentDirectory(), nameOfFile);

        /// <summary>
        /// Абоненты.
        /// </summary>
        internal List<Subscriber> subscribers = new List<Subscriber>();

        /// <summary>
        /// Конструктор.
        /// </summary>
        private PhoneBook() {}

        /// <summary>
        /// Событие при удалении абонента
        /// </summary>
        public event Action<string>? NotifyMessageRemove;

        /// <summary>
        /// Событие при добавлении абонента
        /// </summary>
        public event Action<string>? NotifyMessageAdd;

        /// <summary>
        /// Событие при чтении и записи абонентов из файла
        /// </summary>
        public event Action<string>? NotifyMessage;


        /// <summary>
        /// Считать абонентов из файла.
        /// </summary>
        public void LoadSubscrivers()
        {
            if (File.Exists(filePath))
                using (StreamReader fs = new StreamReader(filePath, true))
                {
                    while (true)
                    {
                        if (fs.EndOfStream) break;
                        string line = fs.ReadLine();
                        string[] Sub = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                        Subscriber subscriber = new Subscriber(Sub[0], Sub[1]);
                        subscribers.Add(subscriber);
                    }
                }
            NotifyMessage?.Invoke("Данные из файла загружены");
        }
        
        /// <summary>
        /// Получить экземпляр класса.
        /// </summary>
        /// <returns>Экземпляр класса.</returns>
        public static PhoneBook GetIntance()
        {
            if (intance == null)
                intance = new PhoneBook();
            return intance;
        }

        /// <summary>
        /// Добавить абонента.
        /// </summary>
        /// <param name="name">Имя абонента.</param>
        /// <param name="phoneNumber">Номер абонента.</param>
        public void AddSubscriber(string name, string phoneNumber)
        {
            if (GetIsSubscriberCorrect(name, phoneNumber))
            {
                Subscriber newSubscriber = new Subscriber(name, phoneNumber);
                subscribers.Add(newSubscriber);
            }
            NotifyMessageAdd?.Invoke("Абонент добавлен");
        }

        /// <summary>
        /// Проверить данные абонента.
        /// </summary>
        /// <param name="name">Имя абонента.</param>
        /// <param name="phoneNumber">Номер абонента.</param>
        /// <returns><c>true></c>, если  данные абонента.</returns>
        private bool GetIsSubscriberCorrect(string name, string phoneNumber)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }

            for (int i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].PhoneNumber == phoneNumber)
                {
                    Console.WriteLine("Такой номер уже существует");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Удалить абонента.
        /// </summary>
        /// <param name="name">Имя абонента.</param>
        /// <returns>Удален ли абонент.</returns>
        public void DeleteSybscriber(string name)
        {
            for (int i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].Name == name)
                {
                    NotifyMessageRemove?.Invoke($"Абонент {subscribers[i].Name}, телефон {subscribers[i].PhoneNumber} удален");
                    subscribers.RemoveAt(i);
                    i = -1;
                }
            }
        }

        /// <summary>
        /// Найти абонента по имени.
        /// </summary>
        /// <param name="name">Имя абонента.</param>
        /// <returns>Абонента</returns>
        public Subscriber GetSubscriberByName(string name)
        {
            for (int i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].Name == name)
                    return subscribers[i];
            }
            return null;
        }

        /// <summary>
        /// Найти абонента по номеру.
        /// </summary>
        /// <param name="phoneNumber">Номер абонента.</param>
        /// <returns>Абонента.</returns>
        public Subscriber GetSubscriberByNumber(string phoneNumber)
        {
            for (int i = 0; i < subscribers.Count; i++)
            {
                if (subscribers[i].PhoneNumber == phoneNumber)
                    return subscribers[i];
            }
            return null;
        }

        /// <summary>
        /// Запиcать данные абонента в файл.
        /// </summary>
        public void WriteToFile()
        {
            using (FileStream file = new FileStream(filePath, FileMode.OpenOrCreate))
            using (StreamWriter sw = new StreamWriter(file))
            {
                for (int i = 0; i < subscribers.Count; i++)
                {
                    sw.WriteLine($"{ subscribers[i].Name}\t{ subscribers[i].PhoneNumber}");
                }
            }
            NotifyMessage?.Invoke("Данные записаны в файл");
        }
    }
}
