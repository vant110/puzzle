using System;
using System.Collections;
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
        public byte Number { get; set; }
        public Point Position { get; set; }
        public Image Image { get; set; }

        public Fragment(byte number, Point position, Image image)
        {
            Number = number;
            Position = position;
            Image = image;           
        }
    }
}
