using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{
    /// <summary>
    /// Класс Subscriber содержит информацию об абонетах и их телефонных номерах.
    /// </summary>
    public class Subscriber
    {
        private string name;

        /// <summary>
        /// Абонент.
        /// </summary>
        public string Name { get { return name; } }

        private string phoneNumber;

        /// <summary>
        /// Номер телефона абонента.
        /// </summary>
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { this.phoneNumber = value; }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Абонент.</param>
        /// <param name="phoneNumber">Номер телефона.</param>
        public Subscriber(string name, string phoneNumber)
        {
            this.name = name;
            this.phoneNumber = phoneNumber;
        }
    }
}
