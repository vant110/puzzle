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
        private static readonly string format = "";

        private static void CreateDirectory(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
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

        public static void SaveImage(Stream s, string path)
        {
            s.Seek(0, SeekOrigin.Begin);

            CreateDirectory(images);
            using var outFileStream = File.Create($"{images}{path}{format}");
            s.CopyTo(outFileStream);
        }

        public static bool ImageExists(string path)
        {
            return File.Exists($"{images}{path}{format}");
        }
        public static bool HelpExists(string[] help)
        {
            bool exists = false;
            for (int i = 0; i < help.Length; i++)
            {
                exists = File.Exists(help[i]);
                if (!exists) break;
            }
            return exists;
        }

        public static MemoryStream LoadImage(string path)
        {
            var ms = new MemoryStream();
            using var inFileStream = File.OpenRead($"{images}{path}{format}");
            inFileStream.CopyTo(ms);
            return ms;
        }
        public static MemoryStream LoadHelp(string[] help)
        {
            var ms = new MemoryStream();
            for (int i = 0; i < help.Length; i++)
            {
                using var inFileStream = File.OpenRead(help[i]);
                inFileStream.CopyTo(ms);
            }
            return ms;
        }

        public static void DeleteImage(string path)
        {
            File.Delete($"{images}{path}{format}");
        }
    }
}
