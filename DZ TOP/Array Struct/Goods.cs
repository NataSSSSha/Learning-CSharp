using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Array_Struct
{
    internal struct Goods
    {
        /// <summary>
        /// Описание товара.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Сумма товара.
        /// </summary>
        public int Sum { get; set; }

        /// <summary>
        /// Товар.
        /// </summary>
        /// <param name="description">Описание.</param>
        /// <param name="sum">Сумма.</param>
        public Goods(string description, int sum)
        {
            Description = description;
            Sum = sum;
        }
    }
}
