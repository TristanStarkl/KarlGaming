using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlGaming
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(10, 10);
            game.Play();

            Console.WriteLine("-----------------------");
            Console.ReadLine();
        }
    }
}
