using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Neuroshima
{
    class GameboardCreator
    {
        private static Dictionary<Point, Rectangle> gameboard;

        public GameboardCreator()
        {
            gameboard = new Dictionary<Point, Rectangle>();
        }

        public void ClearGameboard()
        {
            gameboard.Clear();
        }

        public void AddRectangle(Point point, Rectangle rec)
        {
            gameboard.Add(point, rec);
        }
    }
}
