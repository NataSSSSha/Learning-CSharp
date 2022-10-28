namespace Lesson8_List_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyList <string> myList = new MyList<string>();
            myList.Add ("Bye");
            myList.Add ("Hello");
            myList.Add ("Something else");

            Console.WriteLine(myList.Count);
            Console.WriteLine(myList[1]);


            myList.Delete(1);
            Console.WriteLine(myList.Count);
            Console.WriteLine(myList[1]);


            myList.Delete("Bye");
            Console.WriteLine(myList.Count);

        }
    }
}