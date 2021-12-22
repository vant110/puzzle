using puzzle.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle.DTO
{
    class MyPuzzle
    {
        public static MyPuzzle Instance { get; set; }

        private int fragmentType;
        private int assemblyType;
        private int nHorizontal;
        private int nVertical;
        private int length;
        private Fragment[] field;
        private Fragment[] tape;
        private Image image;

        public int FragmentType { get => fragmentType; set => fragmentType = value; }
        public int AssemblyType { get => assemblyType; set => assemblyType = value; }
        public int NHorizontal { get => nHorizontal; set => nHorizontal = value; }
        public int NVertical { get => nVertical; set => nVertical = value; }
        public Image Image { get => image; set => image = value; }

        public MyPuzzle(
            int fragmentType, 
            int assemblyType, 
            int nHorizontal,
            int nVertical,
            Image image)
        {
            FragmentType = fragmentType;
            AssemblyType = assemblyType;
            NHorizontal = nHorizontal;
            NVertical = nVertical;
            Image = image;

            length = NHorizontal * NVertical;
            if (fragmentType == 2)
            {
                // Треугольные.
                length *= 2;
            }

            field = new Fragment[length];
            if (assemblyType == 2)
            {
                // С ленты.
                tape = new Fragment[length];
            }
        }

        public void SplitIntoFragments()
        {
            Size size = new(
                Image.Width / NHorizontal,
                Image.Height / NVertical);
            using Bitmap bitmap = new(
                size.Width, 
                size.Height);
            bitmap.SetResolution(Image.HorizontalResolution, Image.VerticalResolution);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            using ImageAttributes wrapMode = new();
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            Rectangle destRect = new(
                0, 0,
                bitmap.Width,
                bitmap.Height);
            for (int i = 0; i < length; i++)
            {
                Point position = new(
                    size.Width * i,
                    size.Height * i);
                graphics.DrawImage(
                    Image,
                    destRect,
                    position.X, 
                    position.Y,
                    size.Width,
                    size.Height,
                    GraphicsUnit.Pixel,
                    wrapMode);
                field[i] = new Fragment(i, bitmap);
            }
        }

        public void Mix(Fragment[] arr)
        {
            Random rand = new();
            for (int i = arr.Length - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);
                Fragment tmp = arr[j];
                arr[j] = arr[i];
                arr[i] = tmp;
            }
        }
    }
}
