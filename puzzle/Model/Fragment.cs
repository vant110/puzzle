using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle.Model
{
    class Fragment
    {
        public static Size Size { get; set; }
        public Point OriginPosition { get; set; }
        public Point Position { get; set; }
        public Image Image { get; set; }

        public Fragment(Point originPosition, Image image)
        {
            OriginPosition = originPosition;
            Position = OriginPosition;
            Image = image;           
        }
    }
}
