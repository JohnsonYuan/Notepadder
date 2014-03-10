using System;
using System.Drawing;

namespace Notepadder
{
    public class CarentChangedEventArgs : EventArgs
    {
        public Point Position { get; set; }

        public CarentChangedEventArgs(Point pos)
        {
            Position = pos;
        }

        public CarentChangedEventArgs(int column, int line)
            : this(new Point(column, line))
        {
        }
    }
}
