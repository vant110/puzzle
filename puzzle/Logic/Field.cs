using puzzle.Model;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace puzzle.Logic
{
    class Field
    {
        private readonly Game game;
        private readonly ColorMatrix colorMatrix;
        public sbyte NHorizontal { get; private set; }
        public sbyte NVertical { get; private set; }
        public Fragment[] Fragments { get; private set; }

        public byte[] FragmentNumbers
        {
            get
            {
                byte[] fragmentNumbers = new byte[Fragments.Length];
                for (int i = 0; i < fragmentNumbers.Length; i++)
                {
                    fragmentNumbers[i] = byte.MaxValue;
                }

                for (int i = 0; i < Fragments.Length; i++)
                {
                    if (Fragments[i] is null) continue;
                    fragmentNumbers[Fragments[i].Number] = (byte)i;
                }

                return fragmentNumbers;
            }
            set
            {
                var originalGame = new Game(
                    game.FragmentTypeId,
                    1,
                    NHorizontal,
                    NVertical,
                    game.Image);
                var originalField = new Fragment[Fragments.Length];
                originalGame.Field.Fragments.CopyTo(originalField, 0);

                for (int i = 0; i < value.Length; i++)
                {
                    if (value[i] == byte.MaxValue) continue;

                    Fragments[value[i]] = originalField[i];

                    if (Fragments[value[i]] is not null)
                    {
                        Fragments[value[i]].InOriginalPosition = FragmentInOriginalPosition(value[i]);
                    }
                }
            }
        }

        public Field(
            sbyte nHorizontal,
            sbyte nVertical,
            Fragment[] fragments,
            Game game)
        {
            NHorizontal = nHorizontal;
            NVertical = nVertical;
            Fragments = fragments;
            this.game = game;

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
        }

        public void Draw(Image image)
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
                    if (Fragments[i * NHorizontal + j] is null) continue;

                    if (Fragments[i * NHorizontal + j].InOriginalPosition)
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
                        Fragments[i * NHorizontal + j].Image,
                        destRect,
                        0, 0,
                        Fragment.Size.Width,
                        Fragment.Size.Height,
                        GraphicsUnit.Pixel,
                        imageAttr);
                }
            }
        }
        public int GetFragmentIndex(Point location, Size fieldSize)
        {
            var position = new Point(
                Game.SearchNumber(location.X, fieldSize.Width, NHorizontal),
                Game.SearchNumber(location.Y, fieldSize.Height, NVertical));

            return position.Y * NHorizontal + position.X;
        }
        public void DrawFragment(int index, Image image)
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
            if (Fragments[index] is null)
            {
                SolidBrush solidBrush = new(Color.White);
                graphics.FillRectangle(solidBrush, destRect);
            }
            else
            {
                using ImageAttributes imageAttr = new();
                imageAttr.SetWrapMode(WrapMode.TileFlipXY);
                if (Fragments[index].InOriginalPosition)
                {
                    imageAttr.SetColorMatrix(colorMatrix);
                }

                graphics.DrawImage(
                    Fragments[index].Image,
                    destRect,
                    0, 0,
                    Fragment.Size.Width,
                    Fragment.Size.Height,
                    GraphicsUnit.Pixel,
                    imageAttr);
            }
        }

        public void SwapFragments(int index1, int index2)
        {
            Fragment tmp = Fragments[index1];
            Fragments[index1] = Fragments[index2];
            Fragments[index2] = tmp;

            if (Fragments[index1] is not null)
            {
                Fragments[index1].InOriginalPosition = FragmentInOriginalPosition(index1);
            }
            if (Fragments[index2] is not null)
            {
                Fragments[index2].InOriginalPosition = FragmentInOriginalPosition(index2);
            }
        }

        public bool FragmentInOriginalPosition(int index)
        {
            Point position = new(
                index % NHorizontal,
                index / NHorizontal);

            if (Fragments[index].OriginalPosition.X == position.X
                && Fragments[index].OriginalPosition.Y == position.Y)
            {
                return true;
            }
            return false;
        }
        public bool EachFragmentInOriginalPosition()
        {
            foreach (var fragment in Fragments)
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
