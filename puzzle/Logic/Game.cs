using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace puzzle.Model
{
    class Game
    {
        private readonly ColorMatrix colorMatrix;

        public static Game Instance { get; set; }

        public sbyte FragmentTypeId { get; private set; }
        public sbyte AssemblyTypeId { get; private set; }
        public sbyte NHorizontal { get; private set; }
        public sbyte NVertical { get; private set; }
        public Image Image { get; private set; }
        public Fragment[] Field { get; private set; }
        public Fragment[] Tape { get; private set; }

        public sbyte CountingMethodId { get; set; }
        public short Score { get; set; } = 0;
        public int Time { get; set; } = 0;

        public byte[] FieldFragmentNumbers
        {
            get
            {
                byte[] fragmentNumbers = new byte[Field.Length];
                for (int i = 0; i < fragmentNumbers.Length; i++)
                {
                    fragmentNumbers[i] = byte.MaxValue;
                }

                for (int i = 0; i < Field.Length; i++)
                {
                    if (Field[i] is null) continue;
                    fragmentNumbers[Field[i].Number] = (byte)i;
                }

                return fragmentNumbers;
            }
            set
            {
                var originalGame = new Game(
                    FragmentTypeId,
                    1,
                    NHorizontal,
                    NVertical,
                    Image);
                var originalField = new Fragment[Field.Length];
                originalGame.Field.CopyTo(originalField, 0);

                for (int i = 0; i < value.Length; i++)
                {
                    if (value[i] == byte.MaxValue) continue;

                    Field[value[i]] = originalField[i];

                    if (Field[value[i]] is not null)
                    {
                        Field[value[i]].InOriginalPosition = FragmentInOriginalPosition(value[i]);
                    }
                }
            }
        }
        public byte[] TapeFragmentNumbers
        {
            get
            {
                if (Tape is null) return null;

                byte[] fragmentNumbers = new byte[Field.Length];
                for (int i = 0; i < fragmentNumbers.Length; i++)
                {
                    fragmentNumbers[i] = byte.MaxValue;
                }

                for (int i = 0; i < Tape.Length; i++)
                {
                    fragmentNumbers[Tape[i].Number] = (byte)i;
                }

                return fragmentNumbers;
            }
            set
            {
                var originalTape = new Fragment[Field.Length];
                Tape.CopyTo(originalTape, 0);
                for (int i = 0; i < Tape.Length; i++)
                {
                    Tape[i] = null;
                }

                for (int i = 0; i < value.Length; i++)
                {
                    if (value[i] == byte.MaxValue) continue;

                    Tape[value[i]] = originalTape[i];

                    if (Tape[value[i]] is not null)
                    {
                        Tape[value[i]].InOriginalPosition = false;
                    }
                }
                Tape = Tape.Where(f => f is not null).ToArray();
            }
        }

        public Game(
            sbyte fragmentTypeId,
            sbyte assemblyTypeId,
            sbyte nHorizontal,
            sbyte nVertical,
            Image image)
        {
            FragmentTypeId = fragmentTypeId;
            AssemblyTypeId = assemblyTypeId;
            NHorizontal = nHorizontal;
            NVertical = nVertical;
            Image = image;

            {
                int length = NHorizontal * NVertical;
                if (FragmentTypeId == 2)
                {
                    // Треугольные.
                    length *= 2;
                }

                Field = new Fragment[length];
                if (AssemblyTypeId == 2)
                {
                    // С ленты.
                    Tape = new Fragment[length];
                }
            }

            Fragment.Size = new(
                Image.Width / NHorizontal,
                Image.Height / NVertical);

            colorMatrix = new()
            {
                // Red
                Matrix00 = 1.00f,
                // Green
                Matrix11 = 1.00f,
                // Blue
                Matrix22 = 1.00f,
                // alpha
                Matrix33 = 0.50f,
                // w
                Matrix44 = 1.00f
            };

            SplitIntoFragments();
        }
        private void SplitIntoFragments()
        {
            // Создает изображения фрагментов в обычном порядке.
            Fragment[] arr = AssemblyTypeId == 1
                ? Field
                : Tape;
            for (int i = 0; i < NVertical; i++)
            {
                for (int j = 0; j < NHorizontal; j++)
                {
                    Bitmap bitmap = new(
                        Fragment.Size.Width,
                        Fragment.Size.Height);
                    bitmap.SetResolution(Image.HorizontalResolution, Image.VerticalResolution);

                    using Graphics graphics = Graphics.FromImage(bitmap);
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    using ImageAttributes imageAttr = new();
                    imageAttr.SetWrapMode(WrapMode.TileFlipXY);

                    Rectangle destRect = new(
                        0, 0,
                        bitmap.Width,
                        bitmap.Height);
                    Point position = new(j, i);
                    graphics.Clear(Color.White);
                    graphics.DrawImage(
                        Image,
                        destRect,
                        position.X * Fragment.Size.Width,
                        position.Y * Fragment.Size.Height,
                        Fragment.Size.Width,
                        Fragment.Size.Height,
                        GraphicsUnit.Pixel,
                        imageAttr);
                    byte number = (byte)(i * NHorizontal + j);
                    arr[i * NHorizontal + j] = new Fragment(number, position, bitmap);
                }
            }
        }

        public void Mix()
        {
            Fragment[] arr = AssemblyTypeId == 1
                ? Field
                : Tape;
            Random rand = new();
            for (int i = arr.Length - 1; i >= 1; i--)
            {
                int j = rand.Next(i);

                Fragment tmp = arr[j];
                arr[j] = arr[i];
                arr[i] = tmp;

                arr[i].InOriginalPosition = false;
            }
            arr[0].InOriginalPosition = false;
        }
        public void DrawField(Image image)
        {
            Size fragmentSize = new(
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
                    if (Field[i * NHorizontal + j] is null) continue;

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
                        position.X * fragmentSize.Width,
                        position.Y * fragmentSize.Height,
                        fragmentSize.Width,
                        fragmentSize.Height);
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
        public void DrawTape(Image image)
        {
            Size fragmentSize = new(
                image.Width,
                image.Height / Tape.Length);

            using var graphics = Graphics.FromImage(image);
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using ImageAttributes imageAttr = new();
            imageAttr.SetWrapMode(WrapMode.TileFlipXY);

            for (int i = 0; i < Tape.Length; i++)
            {
                Rectangle destRect = new(
                    0,
                    i * fragmentSize.Height,
                    fragmentSize.Width,
                    fragmentSize.Height);
                graphics.DrawImage(
                    Tape[i].Image,
                    destRect,
                    0, 0,
                    Fragment.Size.Width,
                    Fragment.Size.Height,
                    GraphicsUnit.Pixel,
                    imageAttr);
            }
        }
                
        private static int SearchNumber(int location, int size, int count)
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
        public int GetFragmentIndexOnField(Point location, Size pictureBoxSize)
        {
            var position = new Point(
                SearchNumber(location.X, pictureBoxSize.Width, NHorizontal),
                SearchNumber(location.Y, pictureBoxSize.Height, NVertical));

            return position.Y * NHorizontal + position.X;
        }
        public int GetFragmentIndexOnTape(Point location, Size pictureBoxSize)
        {
            return SearchNumber(location.Y, pictureBoxSize.Height, Tape.Length);
        }

        public void SwapFragmentsOnField(int index1, int index2)
        {
            Fragment tmp = Field[index1];
            Field[index1] = Field[index2];
            Field[index2] = tmp;

            if (Field[index1] is not null)
            {
                Field[index1].InOriginalPosition = FragmentInOriginalPosition(index1);
            }
            if (Field[index2] is not null)
            {
                Field[index2].InOriginalPosition = FragmentInOriginalPosition(index2);
            }
        }
        public void AddFragmentsOnFieldFromTape(int tapeIndex, int fieldIndex)
        {
            Fragment tmp = Tape[tapeIndex];
            Tape[tapeIndex] = Field[fieldIndex];
            Field[fieldIndex] = tmp;

            if (Tape[tapeIndex] is not null)
            {
                Tape[tapeIndex].InOriginalPosition = false;
            }
            if (Field[fieldIndex] is not null)
            {
                Field[fieldIndex].InOriginalPosition = FragmentInOriginalPosition(fieldIndex);
            }
        }

        public void DrawFragmentOnField(int index, Image image)
        {
            Size fragmentsSize = new(
                image.Width / NHorizontal,
                image.Height / NVertical);

            using var graphics = Graphics.FromImage(image);
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Point position = new(
                index % NHorizontal,
                index / NHorizontal);
            Rectangle destRect = new(
                position.X * fragmentsSize.Width,
                position.Y * fragmentsSize.Height,
                fragmentsSize.Width,
                fragmentsSize.Height);
            if (Field[index] is null)
            {
                SolidBrush solidBrush = new(Color.White);
                graphics.FillRectangle(solidBrush, destRect);
            }
            else
            {
                using ImageAttributes imageAttr = new();
                imageAttr.SetWrapMode(WrapMode.TileFlipXY);
                if (Field[index].InOriginalPosition)
                {
                    imageAttr.SetColorMatrix(colorMatrix);
                }

                graphics.DrawImage(
                    Field[index].Image,
                    destRect,
                    0, 0,
                    Fragment.Size.Width,
                    Fragment.Size.Height,
                    GraphicsUnit.Pixel,
                    imageAttr);
            }
        }
        public void DrawFragmentOnTape(int index, Image image)
        {
            Size fragmentSize = new(
                image.Width,
                image.Height / Tape.Length);

            using var graphics = Graphics.FromImage(image);
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using ImageAttributes imageAttr = new();
            imageAttr.SetWrapMode(WrapMode.TileFlipXY);

            Rectangle destRect = new(
                0,
                index * fragmentSize.Height,
                fragmentSize.Width,
                fragmentSize.Height);
            graphics.DrawImage(
                Tape[index].Image,
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
                if (fragment is null
                    || !fragment.InOriginalPosition)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
