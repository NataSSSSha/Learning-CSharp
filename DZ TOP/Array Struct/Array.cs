using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Array_Struct
{
    internal class Array
    {
        /// <summary>
        /// Массив чисел.
        /// </summary>
        internal int[] arr = new int[100];

        /// <summary>
        /// Генерирует случайные числа.
        /// </summary>
        Random r = new Random();

        /// <summary>
        /// Минимальное значение в массиве.
        /// </summary>
        public const int minValueInArray = 0;

        /// <summary>
        /// Максимальное значение в массиве.
        /// </summary>
        public const int maxValueInArray = 9;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public Array()
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = r.Next(minValueInArray, maxValueInArray);
        }

        /// <summary>
        /// Вычислить количество определенного значения в массиве.
        /// </summary>
        /// <param name="value">Значение, по которому происходит поиск.</param>
        /// <returns>Количество значений в массиве.</returns>
        public int CountInArray(int value)
        {
            int count = 0;
            for(int i = 0; i < arr.Length; i++)
            {
                if (value == arr[i])
                    count++;
            }
            return count;
        }
    }
}
