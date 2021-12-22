using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace puzzle.Services
{
    static class LocalStorage
    {
        private static readonly string images = Application.StartupPath + "images\\";
        private static readonly string format = ".png";

        private static void CreateDirectory()
        {
            var directoryInfo = new DirectoryInfo(images);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }

        public static MemoryStream Resize(Stream imageStream)
        {
            using Bitmap bitmap = new(600, 450);
            using (Image image = Image.FromStream(imageStream))
            {
                imageStream.Close();
                Rectangle destRect;
                {
                    int x0 = 0;
                    int y0 = 0;
                    int x1 = bitmap.Width;
                    int y1 = bitmap.Height;
                    {
                        int dx;
                        int dy;
                        double k;
                        {
                            double kx = (double)bitmap.Width / image.Width;
                            double ky = (double)bitmap.Height / image.Height;
                            k = kx > ky ? kx : ky;
                        }
                        if (image.Width < bitmap.Width
                            || image.Height < bitmap.Height)
                        {
                            dx = (int)((image.Width * k - bitmap.Width) * k / 2);
                            dy = (int)((image.Height * k - bitmap.Height) * k / 2);
                        }
                        else
                        {
                            dx = (int)((image.Width * k - bitmap.Width) / k / 2);
                            dy = (int)((image.Height * k - bitmap.Height) / k / 2);
                        }
                        x0 -= dx / 2;
                        y0 -= dy / 2;
                        x1 += dx;
                        y1 += dy;
                    }
                    destRect = new(
                        x0, y0,
                        x1, y1);
                }
                bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                using Graphics graphics = Graphics.FromImage(bitmap);
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using ImageAttributes wrapMode = new();
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(
                    image,
                    destRect,
                    0, 0,
                    image.Width,
                    image.Height,
                    GraphicsUnit.Pixel,
                    wrapMode);
            }
            MemoryStream ms = new();
            bitmap.Save(ms, ImageFormat.Png);
            return ms;
        }

        public static void Save(Stream imageStream, string path)
        {
            CreateDirectory();
            using var outFileStream = File.Create($"{images}{path}{format}");
            imageStream.CopyTo(outFileStream);
            imageStream.Close();
        }

        public static MemoryStream Load(string path)
        {
            var ms = new MemoryStream();
            using var inFileStream = File.OpenRead($"{images}{path}{format}");
            inFileStream.CopyTo(ms);
            return ms;
        }

        public static Image LoadImage(string path)
        {
            return Image.FromFile($"{images}{path}{format}");
        }

        public static bool Exists(string path)
        {
            return File.Exists($"{images}{path}{format}");
        }

        public static void Delete(string path)
        {
            File.Delete($"{images}{path}{format}");
        }
    }
}
