using System;
using System.Threading;

namespace Tamagotchi
{
    internal class Program
    {

        public static Animal StartGame()
        {
            Console.WriteLine("Выбери питомца:\n 1 - Котенок\n 2 - Щенок\n 3 - Хомячок");

            bool isCorrectPet = int.TryParse(Console.ReadLine(), out int chooseAnimal);

            Console.WriteLine("Дай имя своему питомцу");

            string? animalName = Console.ReadLine();

            Animal animal;

            switch (chooseAnimal)
            {
                case 1: animal = new Cat(animalName); return animal;
                case 2: animal = new Dog(animalName); return animal; 
                case 3: animal = new Humster(animalName); return animal; 
                default: return null;
            }
        }

        static void Main(string[] args)
        {
            Animal pet;
            do
            { 
                pet = StartGame();
                if (pet == null)
                    Console.WriteLine("Кажется, ты нажал что-то не то... Попробуй еще раз");
            }while (pet == null);

            Console.WriteLine("Отлично! Теперь ты можешь:\n" +
                             "Напоить питомца, нажав - 1\n" +
                             "Покормить питомца, нажав - 2\n" +
                             "Поиграть с питомцем, нажав - 3\n" +
                             "Нажми 0, чтобы выйти\n");

            int choose;

            do
            {
                bool success = int.TryParse(Console.ReadLine(), out choose);

                switch (choose)
                {
                    case 1:
                        pet.Drink();
                        break;
                    case 2:
                        pet.Eat();
                        break;
                    case 3:
                        pet.Play();
                        break;
                    default:
                        break;
                }
            } while (choose != 0);
        }
    }
}