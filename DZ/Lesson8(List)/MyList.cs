using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson8_List_
{
    /// <summary>
    /// Динамический массив.
    /// </summary>
    /// <typeparam name="T">Тип данных массива.</typeparam>
    internal class MyList<T>
    {
        /// <summary>
        /// Размер массива.
        /// </summary>
        static int size = 1;

        /// <summary>
        /// Массив.
        /// </summary>
        T[] values= new T[size];

        /// <summary>
        /// Количество элементов массива.
        /// </summary>
        int count =0;
        public int Count { get { return count; } }

        /// <summary>
        /// Индексатор.
        /// </summary>
        public T this[int index]
        {
            get => values[index];
            set => values[index] = value;
        }

        /// <summary>
        /// Добавляет значение в массив.
        /// </summary>
        /// <param name="value">Значение.</param>
        public void Add(T value)
        {
            values[size-1] = value;
            size++;
            Array.Resize(ref values, size);
            count++;
        }

        /// <summary>
        /// Удаляет элемент массива по индексу.
        /// </summary>
        /// <param name="index">Индекс элемента массива.</param>
        public void Delete(int index)
        {
            try
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException("Индекс вне диапазона массива");
                if (count == 0)
                    throw new Exception("В масссиве нет данных");
                else
                    while (index < count)
                    {
                        values[index] = values[index + 1];
                        index++;
                    }
                Array.Resize(ref values, size--);
                count--;
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Удаляет элемент массива по значению.
        /// </summary>
        /// <param name="value">Значение элемента массива.</param>
        public void Delete(T value)
        {
            for (int i = 0; i < count; i++)
            {
                if (Equals(value, values[i]))
                    Delete(i);
            }
        }
    }
}
