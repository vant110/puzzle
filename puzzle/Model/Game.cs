using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace puzzle.Model
{
    class Game
    {
        private ColorMatrix colorMatrix;

        public static Game Instance { get; set; }

        public int FragmentType { get; set; }
        public int AssemblyType { get; set; }
        public int NHorizontal { get; set; }
        public int NVertical { get; set; }
        public int Length { get; set; }
        public Fragment[] Field { get; set; }
        public Fragment[] Tape { get; set; }
        public Image MyImage { get; set; }

        public sbyte CountingMethodId { get; set; }
        public short Score { get; set; }
        public int Time { get; set; }

        public Game(
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


            colorMatrix = new();
            // Red
            colorMatrix.Matrix00 = 1.00f;
            // Green
            colorMatrix.Matrix11 = 1.00f;
            // Blue
            colorMatrix.Matrix22 = 1.00f;
            // alpha
            colorMatrix.Matrix33 = 0.50f;
            // w
            colorMatrix.Matrix44 = 1.00f;


            SplitIntoFragments();
        }

        private void SplitIntoFragments()
        {
            // Создает изображения фрагментов в обычном порядке.
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
                    byte number = (byte)(i * NHorizontal + j);
                    arr[i * NHorizontal + j] = new Fragment(number, position, bitmap);
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

                arr[i].InOriginalPosition = false;
            }
        }
        public void DrawField(Image image)
        {
            Size sizeGraphics = new(
                image.Width / NHorizontal,
                image.Height / NVertical);

            using var graphics = Graphics.FromImage(image);
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using ImageAttributes imageAttr = new();
            imageAttr.SetWrapMode(WrapMode.TileFlipXY);

            for (int i = 0; i < NVertical; i++)
            {
                for (int j = 0; j < NHorizontal; j++)
                {
                    if (Field[i * NHorizontal + j].InOriginalPosition)
                    {
                        imageAttr.SetColorMatrix(colorMatrix);
                    }
                    else
                    {
                        imageAttr.ClearColorMatrix();
                    }
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
                        imageAttr);
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
                    fragmentNumbers[arr[i].Number] = (byte)i;
                }
                return fragmentNumbers;
            }
            set
            {
                Fragment[] arr = Field;
                if (AssemblyType == 2)
                {
                    arr = Tape;
                }

                var oldArr = new Fragment[Length];
                arr.CopyTo(oldArr, 0);
                for (int i = 0; i < Length; i++)
                {
                    arr[value[i]] = oldArr[i];
                    arr[value[i]].InOriginalPosition = FragmentInOriginalPosition(value[i]);
                }
            }
        }

        public int GetFragmentIndex(Point location, Size boxSize)
        {
            static int SearchNumber(int location, int size, int count)
            {
                int i;
                int step = size / count;
                for (i = 0; i < step - 1; i++)
                {
                    if (location < (i + 1) * step)
                    {
                        break;
                    }
                }
                return i;
            }

            var position = new Point(
                SearchNumber(location.X, boxSize.Width, NHorizontal),
                SearchNumber(location.Y, boxSize.Height, NVertical));

            return position.Y * NHorizontal + position.X;
        }
        public void SwapFragments(int index1, int index2)
        {
            Fragment tmp = Field[index1];
            Field[index1] = Field[index2];
            Field[index2] = tmp;

            Field[index1].InOriginalPosition = FragmentInOriginalPosition(index1);
            Field[index2].InOriginalPosition = FragmentInOriginalPosition(index2);
        }
        public void DrawFragment(int index, Image image)
        {            
            Size sizeGraphics = new(
                image.Width / NHorizontal,
                image.Height / NVertical);

            using var graphics = Graphics.FromImage(image);
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using ImageAttributes imageAttr = new();
            imageAttr.SetWrapMode(WrapMode.TileFlipXY);
            if (Field[index].InOriginalPosition)
            {
                imageAttr.SetColorMatrix(colorMatrix);
            }

            Point position = new(
                index % NHorizontal,
                index / NHorizontal);
            Rectangle destRect = new(
                position.X * sizeGraphics.Width,
                position.Y * sizeGraphics.Height,
                sizeGraphics.Width,
                sizeGraphics.Height);
            graphics.DrawImage(
                Field[index].Image,
                destRect,
                0, 0,
                Fragment.Size.Width,
                Fragment.Size.Height,
                GraphicsUnit.Pixel,
                imageAttr);
        }
        public bool FragmentInOriginalPosition(int index)
        {
            Point position = new(
                index % NHorizontal,
                index / NHorizontal);

            if (Field[index].OriginalPosition.X == position.X
                && Field[index].OriginalPosition.Y == position.Y)
            {
                return true;
            }
            return false;
        }
        public bool AllFragmentsInOriginalPosition()
        {
            foreach (var fragment in Field)
            {
                if (!fragment.InOriginalPosition)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
