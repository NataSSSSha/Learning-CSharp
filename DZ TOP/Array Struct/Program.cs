namespace Array_Struct
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---- 1. Программа, которая считает, сколько раз это число встречается в массиве ----");
            var newArray = new Array();
            Console.WriteLine($"Введите число от {Array.minValueInArray} до {Array.maxValueInArray}");
            if (int.TryParse(Console.ReadLine(), out int value))
                Console.WriteLine($"Число {value} встречается в массиве {newArray.CountInArray(value)} раз\n");

            Console.WriteLine("---- 2.3. Структуры Client и Request ----");
            var client = new Client("Анатолий", 1);
            DateTime date = DateTime.Now;
            var request = new Request(1, date);
            request.client = client;
            client.CountOfRequests += 1;
            var goods = new Goods("Обувь", 10000);
            request.Goods.Add(goods);
            var goods1 = new Goods("Платье", 20000);
            request.Goods.Add(goods1);
            client.SumOfRequests += request.Sum;
            Console.WriteLine($"Заказ {request.ID} от {request.dateTime.ToShortDateString()}, клиент {request.client.ID} - {request.client.Name} : сумма {request.Sum}");

        }
    }
}