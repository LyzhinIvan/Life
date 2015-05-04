using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Life1
{
    public static class Ext
    {
        public static HashSet<Point> Execute(this HashSet<Point> points, Action<Point> action)
        {
            foreach (var point in points)
                action(point);
            return points;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            //Point[] points = new[] {new Point(4, 5), new Point(4, 6), new Point(4, 7)};
            Point[] points = new[]
            {new Point(16, 15), new Point(16, 16), new Point(17, 14), new Point(17, 15), new Point(18, 15)};
            Game game = new Game(points);
            Run(game);
        }

        private static void Run(Game game)
        {
            while (true)
                GoToNext(game);
        }

        private static void GoToNext(Game game)
        {
            game.Update();
            Print(game.LiveCells);
            Thread.Sleep(500);
        }

        private static void Print(HashSet<Point> liveCells)
        {
            Console.Clear();
            new HashSet<Point>(liveCells.Where(p => p.X>=0 && p.Y>=0)).Execute(p =>
            {
                Console.SetCursorPosition(p.X, p.Y);
                Console.Write("X");
            });
        }
    }
}
