using puzzle.Logic;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace puzzle.Model
{
    class Game
    {
        public static Game Instance { get; set; }

        public sbyte FragmentTypeId { get; private set; }
        public sbyte AssemblyTypeId { get; private set; }
        public Image Image { get; private set; }
        public Field Field { get; private set; }
        public Tape Tape { get; private set; }

        public sbyte CountingMethodId { get; set; }
        public short Score { get; set; } = 0;
        public int Time { get; set; } = 0;

        public static int SearchNumber(int location, int size, int count)
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

        public Game(
            sbyte fragmentTypeId,
            sbyte assemblyTypeId,
            sbyte nHorizontal,
            sbyte nVertical,
            Image image)
        {
            FragmentTypeId = fragmentTypeId;
            AssemblyTypeId = assemblyTypeId;
            Image = image;

            {
                int length = nHorizontal * nVertical;
                Field = new(nHorizontal, nVertical, new Fragment[length], this);
                if (AssemblyTypeId == 2)
                {
                    Tape = new(new Fragment[length], this);
                }
            }

            CreateFragments();
        }
        private void CreateFragments()
        {
            // Создает изображения фрагментов в обычном порядке.
            Fragment.Size = new(
                Image.Width / Field.NHorizontal,
                Image.Height / Field.NVertical);

            Fragment[] arr = AssemblyTypeId == 1
                ? Field.Fragments
                : Tape.Fragments;
            for (int i = 0; i < Field.NVertical; i++)
            {
                for (int j = 0; j < Field.NHorizontal; j++)
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
                    byte number = (byte)(i * Field.NHorizontal + j);
                    arr[i * Field.NHorizontal + j] = new Fragment(number, position, bitmap);
                }
            }
        }

        public void Mix()
        {
            Fragment[] arr = AssemblyTypeId == 1
                ? Field.Fragments
                : Tape.Fragments;
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

        public void AddFragmentOnFieldFromTape(int tapeIndex, int fieldIndex)
        {
            Fragment tmp = Tape.Fragments[tapeIndex];
            Tape.Fragments[tapeIndex] = Field.Fragments[fieldIndex];
            Field.Fragments[fieldIndex] = tmp;

            if (Tape.Fragments[tapeIndex] is not null)
            {
                Tape.Fragments[tapeIndex].InOriginalPosition = false;
            }
            else
            {
                Tape.DeleteNullFragments();
            }
            if (Field.Fragments[fieldIndex] is not null)
            {
                Field.Fragments[fieldIndex].InOriginalPosition = Field.FragmentInOriginalPosition(fieldIndex);
            }
        }
        public void AddFragmentOnTape(int fieldIndex)
        {
            Tape.AddNullFragment();
            AddFragmentOnFieldFromTape(Tape.Fragments.Length - 1, fieldIndex);
        }
    }
}
