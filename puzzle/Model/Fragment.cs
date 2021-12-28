using System.Drawing;

namespace puzzle.Model
{
    class Fragment
    {
        public static Size Size { get; set; }
        public byte Number { get; set; }
        public Point OriginalPosition { get; set; }
        public Image Image { get; set; }
        public bool InOriginalPosition { get; set; }

        public Fragment(byte number, Point originalPosition, Image image)
        {
            Number = number;
            OriginalPosition = originalPosition;
            Image = image;
            InOriginalPosition = true;
        }
    }
}
