using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace puzzle.Model
{
    class MyPuzzle
    {
        public static MyPuzzle Instance { get; set; }

        public int FragmentType { get; set; }
        public int AssemblyType { get; set; }
        public int NHorizontal { get; set; }
        public int NVertical { get; set; }
        public int Length { get; set; }
        public Fragment[] Field { get; set; }
        public Fragment[] Tape { get; set; }
        public Image MyImage { get; set; }

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
            MyImage = image;

            Length = NHorizontal * NVertical;
            if (FragmentType == 2)
            {
                // Треугольные.
                Length *= 2;
            }

            Field = new Fragment[Length];
            if (AssemblyType == 2)
            {
                // С ленты.
                Tape = new Fragment[Length];
            }

            Fragment.Size = new(
                MyImage.Width / NHorizontal,
                MyImage.Height / NVertical);
        }

        public void SplitIntoFragments()
        {
            Fragment[] arr = Field;
            if (AssemblyType == 2)
            {
                arr = Tape;
            }
            Size size = new(
                MyImage.Width / NHorizontal,
                MyImage.Height / NVertical);
            for (int i = 0; i < NVertical; i++)
            {
                for (int j = 0; j < NHorizontal; j++)
                {
                    Bitmap bitmap = new(
                        size.Width,
                        size.Height);
                    bitmap.SetResolution(MyImage.HorizontalResolution, MyImage.VerticalResolution);
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
                    Point position = new(j, i);
                    graphics.Clear(Color.White);
                    graphics.DrawImage(
                        MyImage,
                        destRect,
                        position.X * size.Width,
                        position.Y * size.Height,
                        size.Width,
                        size.Height,
                        GraphicsUnit.Pixel,
                        wrapMode);
                    arr[i * NHorizontal + j] = new Fragment(position, bitmap);
                }
            }
        }

        public void Mix()
        {
            Fragment[] arr = Field;
            if (AssemblyType == 2)
            {
                arr = Tape;
            }
            Random rand = new();
            for (int i = arr.Length - 1; i >= 1; i--)
            {
                int j = rand.Next(i);
                Fragment tmp = arr[j];
                arr[j] = arr[i];
                arr[i] = tmp;
            }
        }

        public void DrawField(Graphics graphics)
        {
            graphics.Clear(Color.White);
            Size sizeGraphics = new(
                368 / NHorizontal,
                276 / NVertical);
            graphics.CompositingMode = CompositingMode.SourceOver;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            using ImageAttributes wrapMode = new();
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            for (int i = 0; i < NVertical; i++)
            {
                for (int j = 0; j < NHorizontal; j++)
                {
                    Point position = new(j, i);
                    Rectangle destRect = new(
                        position.X * sizeGraphics.Width,
                        position.Y * sizeGraphics.Height,
                        sizeGraphics.Width,
                        sizeGraphics.Height);
                    graphics.DrawImage(
                        Field[i * NHorizontal + j].Image,
                        destRect,
                        0, 0,
                        Fragment.Size.Width,
                        Fragment.Size.Height,
                        GraphicsUnit.Pixel,
                        wrapMode);
                }
            }
        }

        public byte[] FragmentNumbers
        {
            get
            {
                Fragment[] arr = Field;
                if (AssemblyType == 2)
                {
                    arr = Tape;
                }
                byte[] fragmentNumbers = new byte[Length];
                for (int i = 0; i < Length; i++)
                {
                    fragmentNumbers[i] = (byte)(arr[i].Position.Y * NHorizontal + arr[i].Position.X);
                }
                return fragmentNumbers; 
            }
            set
            {
                if (value.Length != Length) return;
                Fragment[] arr = Field;
                if (AssemblyType == 2)
                {
                    arr = Tape;
                }
                byte[] fragmentNumbers = value;                
                for (int i = 0; i < Length; i++)
                {
                    arr[i].Position = new Point(fragmentNumbers[i] % NHorizontal, fragmentNumbers[i] / NHorizontal);
                }
            }
        }
    }
}
