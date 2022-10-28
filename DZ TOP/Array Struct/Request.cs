using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Array_Struct
{
    internal struct Request
    {
        /// <summary>
        /// Код заказа.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Клиент.
        /// </summary>
        public Client client = new Client("0", 0);

        /// <summary>
        /// Дата заказа.
        /// </summary>
        public DateTime dateTime;

        /// <summary>
        /// Товары в заказе.
        /// </summary>
        public List<Goods> Goods = new List<Goods>();

        /// <summary>
        /// Количество посчитанных товаров.
        /// </summary>
        private int numberSum = 0;

        /// <summary>
        /// Сумма заказа.
        /// </summary>
        private int sum = 0;
        public int Sum
        {
            get
            {
                for(int i = numberSum; i < Goods.Count; i++, numberSum++)
                    sum += Goods[i].Sum;
                return sum;
            }
        }

        /// <summary>
        /// Заказ.
        /// </summary>
        /// <param name="ID">Код заказа.</param>
        /// <param name="date">Дата заказа.</param>
        public Request(int ID, DateTime date)
        {
            this.ID = ID;
            dateTime = date;
        }
    }
}
