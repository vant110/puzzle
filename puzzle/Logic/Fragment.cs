using System.Drawing;

namespace puzzle.Model
{
    class Fragment
    {
        public static Size Size { get; set; }

        public byte Number { get; private set; }
        public Point OriginalPosition { get; private set; }
        public Image Image { get; private set; }

        public bool InOriginalPosition { get; set; } = false;

        public Fragment(byte number, Point originalPosition, Image image)
        {
            Number = number;
            OriginalPosition = originalPosition;
            Image = image;
        }
    }
}
