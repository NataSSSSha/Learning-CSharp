using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Array_Struct
{
    internal struct Client
    {
        /// <summary>
        /// Код клиента.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Имя клиента.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Адрес клиента.
        /// </summary>
        public string? Adress { get; set; } = null;

        /// <summary>
        /// Количество заказов.
        /// </summary>
        public int CountOfRequests { get; set; } = 0;

        /// <summary>
        /// Сумма заказов.
        /// </summary>
        public int SumOfRequests { get; set; } = 0;

        /// <summary>
        /// Клиент.
        /// </summary>
        /// <param name="name">Имя клиента.</param>
        /// <param name="ID">Код клиента.</param>
        public Client(string name, int ID) 
        { 
            Name = name;
            this.ID = ID;
        }
    }
}
