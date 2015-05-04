using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life1
{
    public class Game
    {
        public HashSet<Point> LiveCells { get; private set; }

        public Game(params Point[] startLiveCells)
        {
            LiveCells = new HashSet<Point>(startLiveCells);
        }

        public void Update()
        {
            LiveCells = NextState();
        }

        private HashSet<Point> NextState()
        {
            var nextState = new HashSet<Point>();
            foreach (var cell in LiveCells.SelectMany(GetNearCells).Where(WillCellLive)) nextState.Add(cell);
            return nextState;
        }

        private IEnumerable<Point> GetNearCells(Point cell)
        {
            var d = new[] {-1, 0, 1};
            return (from dx in d from dy in d select new Point(cell.X + dx, cell.Y + dy)).ToList();
        }

        private int CountNearLiveCells(Point cell)
        {
            var count = GetNearCells(cell).Where(LiveCells.Contains).Count();
            return LiveCells.Contains(cell) ? count - 1 : count;
        }

        private bool WillCellLive(Point cell)
        {
            var count = CountNearLiveCells(cell);
            if (LiveCells.Contains(cell) && (count == 2 || count == 3)) return true;
            return !LiveCells.Contains(cell) && count == 3;
        }

        
    }
}
