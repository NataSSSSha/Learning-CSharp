namespace Lesson7
{
    public abstract class Animal
    {
        private string name;
        public string Name { get; set; }

        private double age;
        public double Age { get; set; }

        private string color;
        protected string Color { get; set; }

        public Animal(string name, double age, string color)
        {
            this.name = name;
            this.age = age;
            this.color = color;
        }
        public abstract void Move();

        public abstract void MakeSound();
    }

    class Cat : Animal
    {
        private int coat;

        public int Coat { get { return coat; } set { coat = value; } }

        public Cat(string name, double age, string color, int coat) : base(name, age, color)
        {
            this.coat = coat;
        }

        public override void Move()
        {
            Console.WriteLine("I'm scratching");
        }

        public override void MakeSound()
        {
            Console.WriteLine("Meow");
        }
    }

    class BritishCat : Cat
    {
        private const int undercoat = 10;

        public int Undercoat { get { return undercoat; } }

        public BritishCat(string name, double age, string color, int coat) : base(name, age, color, coat) { }
    }

    class Snake : Animal
    {
        public Snake(string name, double age, string color) : base(name, age, color) { }

        public override void Move()
        {
            Console.WriteLine("I'm crawling");
        }

        public override void MakeSound()
        {
            Console.WriteLine("Shhhh");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Cat someCat = new BritishCat("Vasya", 3, "white", 4);

            Snake someSnake = new Snake("Sasha", 1, "green");

            someSnake.Move();
            someCat.MakeSound();
        }
    }
}