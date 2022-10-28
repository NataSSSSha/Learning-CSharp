using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    internal class Humster : Animal
    {
        /// <summary>
        /// Конструктор хомяка.
        /// </summary>
        /// <param name="name">Имя хомяка.</param>
        public Humster(string name) : base(name) { }

        /// <summary>
        /// Издать хомячий звук.
        /// </summary>
        public override void MakeSound() => Console.WriteLine("-Ф-ф-ф\n");
    }
}
