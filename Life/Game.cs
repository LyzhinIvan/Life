using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using NUnit.Framework;

namespace Life1
{
    public class Game
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool[,] Field { get; private set; }
    

        public Game(int width, int height, params Point[] startPoints)
        {
            if (width == 0 || height == 0) throw new Exception("Размер не может быть нулевым!!!");

            Width = width;
            Height = height;
            Field = new bool[Height, Width];
            foreach (var point in startPoints)
            {
                Field[point.X, point.Y] = true;
            }
        }

        public bool this[int row, int column]
        {
            get { return Field[row, column]; }
        }

        public void NextStep()
        {
            bool[,] nextField = new bool[Height, Width];
            for(int i=0; i<Height; ++i)
                for (int j = 0; j < Width; ++j)
                {
                    nextField[i, j] = Field[i, j];
                    var tempP = CountNeighbours(new Point(i, j));

                    if (Field[i, j] == true && tempP < 2)
                    {
                        nextField[i, j] = false;
                    }
                    if (Field[i, j] == true && tempP > 3)
                    {
                        nextField[i, j] = false;
                    }

                    if (Field[i, j] == false && tempP == 3)
                    {
                        nextField[i, j] = true;
                    }
                }
            Field = nextField;
        }

        private int CountNeighbours(Point p)
        {
            int count = 0;
            for(int i=-1; i<=1; i++)
                for (int j = -1; j <= 1; j++)
                {
                    if(i==0 && j==0) continue;
                    if (CheckBounds(new Point(p.X + i, p.Y + j)))
                        count += (Field[p.X + i, p.Y + j] ? 1 : 0);
                }
            return count;
        }

        private bool CheckBounds(Point p)
        {
            return p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height;
        }

        public void PrintField(TextWriter tw)
        {
            for (int i = 0; i < Height; ++i)
            {
                for (int j = 0; j < Width; ++j)
                {
                    tw.Write(Field[i, j] ? "X" : "0");
                }
                tw.Write("\n");
            }
        }
    }

    public class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    [TestFixture]
    public class Game_should
    {
        [Test]
        public void NotChangeEmptyField()
        {
            Game game = new Game(2, 2);
            game.NextStep();
            for(int i=0; i<game.Height; ++i)
                for (int j = 0; j < game.Width; ++j)
                    Assert.That(game[i, j], Is.EqualTo(false));
        }

        [Test]
        [TestCase(0,0)]
        [TestCase(0,5)]
        [TestCase(5,0)]
        public void ThrowExceptionIfSizeZero(int width, int height)
        {
            Assert.Throws<Exception>(() => new Game(width, height));
        }

        [Test]
        public void NotChangeBlock()
        {
            var game = new Game(4, 4, new Point(1, 1), new Point(1, 2), new Point(2, 1), new Point(2, 2));
            var startField = game.Field;
            game.NextStep();
            for(int i=0; i<game.Height; ++i)
                for (int j = 0; j < game.Width; ++j)
                    Assert.That(game[i, j], Is.EqualTo(startField[i, j]));
            
        }
    }
}
