using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Life1
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(10, 10, new Point(5, 5), new Point(5, 6), new Point(5, 7));
            while (true)
            {
                Console.Clear();
                game.PrintField(Console.Out);
                Thread.Sleep(1000);
                game.NextStep();
            }
        }
    }
}
