using puzzle.Model;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace puzzle.Logic
{
    class Tape
    {
        private readonly Game game;
        public Fragment[] Fragments { get; private set; }

        public byte[] FragmentNumbers
        {
            get
            {
                if (Fragments is null) return null;

                byte[] fragmentNumbers = new byte[game.Field.Fragments.Length];
                for (int i = 0; i < fragmentNumbers.Length; i++)
                {
                    fragmentNumbers[i] = byte.MaxValue;
                }

                for (int i = 0; i < Fragments.Length; i++)
                {
                    fragmentNumbers[Fragments[i].Number] = (byte)i;
                }

                return fragmentNumbers;
            }
            set
            {
                var originalTape = new Fragment[game.Field.Fragments.Length];
                Fragments.CopyTo(originalTape, 0);
                for (int i = 0; i < Fragments.Length; i++)
                {
                    Fragments[i] = null;
                }

                for (int i = 0; i < value.Length; i++)
                {
                    if (value[i] == byte.MaxValue) continue;

                    Fragments[value[i]] = originalTape[i];

                    if (Fragments[value[i]] is not null)
                    {
                        Fragments[value[i]].InOriginalPosition = false;
                    }
                }
                Fragments = Fragments.Where(f => f is not null).ToArray();
            }
        }

        public Tape(
            Fragment[] fragments,
            Game game)
        {
            Fragments = fragments;
            this.game = game;
        }

        public void Draw(Image image)
        {
            Size fragmentSize = new(
                image.Width,
                image.Height / Fragments.Length);

            using var graphics = Graphics.FromImage(image);
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using ImageAttributes imageAttr = new();
            imageAttr.SetWrapMode(WrapMode.TileFlipXY);

            for (int i = 0; i < Fragments.Length; i++)
            {
                Rectangle destRect = new(
                    0,
                    i * fragmentSize.Height,
                    fragmentSize.Width,
                    fragmentSize.Height);
                graphics.DrawImage(
                    Fragments[i].Image,
                    destRect,
                    0, 0,
                    Fragment.Size.Width,
                    Fragment.Size.Height,
                    GraphicsUnit.Pixel,
                    imageAttr);
            }
        }
        public int GetFragmentIndex(Point location, Size tapeSize)
        {
            return Game.SearchNumber(location.Y, tapeSize.Height, Fragments.Length);
        }
        public void DrawFragment(int index, Image image)
        {
            Size fragmentSize = new(
                image.Width,
                image.Height / Fragments.Length);

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
                Fragments[index].Image,
                destRect,
                0, 0,
                Fragment.Size.Width,
                Fragment.Size.Height,
                GraphicsUnit.Pixel,
                imageAttr);
        }

        public void DeleteNullFragments()
        {
            Fragments = Fragments.Where(f => f is not null).ToArray();
        }
        public void AddNullFragment()
        {
            var tapeList = Fragments.ToList();
            tapeList.Add(null);
            Fragments = tapeList.ToArray();
        }
    }
}
