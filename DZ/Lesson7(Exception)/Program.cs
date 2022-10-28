namespace Lesson7_Exception_
{
    class EmptyStringException:Exception
    {
        public EmptyStringException(string message) : base(message) { }
    }
    internal class Program
    {
        /// <summary>
        /// Считывает имя пользователя.
        /// </summary>
        /// <returns>Имя пользователя.</returns>
        public static string ReadUserName()
        {
            string? UserName = String.Empty;
            try
            {
                Console.WriteLine("Введите имя");

                UserName = Console.ReadLine();
                if (UserName == String.Empty)
                {
                    throw new EmptyStringException("Введена пустая строка");
                }
            }
            catch (EmptyStringException e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            finally
            {
                Console.WriteLine(UserName);
            }
            return UserName;
        }
        static void Main(string[] args)
        {
            ReadUserName();
        }
    }
}