using puzzle.Model;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace puzzle.Services
{
    static class LocalStorage
    {
        private static string imagesDir = Application.StartupPath + "images\\";
        private static string format = ".png";

        private static void CreateDirectory()
        {
            var directoryInfo = new DirectoryInfo(imagesDir);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }

        public static MemoryStream ResizeImage(Stream imageStream)
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
                    int dx;
                    int dy;
                    if (image.Width < bitmap.Width
                        || image.Height < bitmap.Height)
                    {
                        double kx = (double)bitmap.Width / image.Width;
                        double ky = (double)bitmap.Height / image.Height;
                        double k = ky > kx ? ky : kx;
                        dx = (int)((image.Width * k - bitmap.Width) * k / 2);
                        dy = (int)((image.Height * k - bitmap.Height) * k / 2);
                    }
                    else
                    {
                        double kx = (double)image.Width / bitmap.Width;
                        double ky = (double)image.Height / bitmap.Height;
                        double k = ky < kx ? ky : kx;
                        dx = (int)((image.Width / k - bitmap.Width) * k / 2);
                        dy = (int)((image.Height / k - bitmap.Height) * k / 2);
                    }
                    x0 -= dx / 2;
                    x1 += dx;
                    y0 -= dy / 2;
                    y1 += dy;
                    destRect = new(
                        x0, y0, 
                        x1, y1);
                }
                Rectangle srcRect = new(
                    0, 0, 
                    image.Width, 
                    image.Height);
                using Graphics graphics = Graphics.FromImage(bitmap);
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(
                    image,
                    destRect,
                    srcRect,
                    GraphicsUnit.Pixel);
            }
            MemoryStream ms = new();
            bitmap.Save(ms, ImageFormat.Png);
            return ms;
        }

        public static void SaveImage(Stream imageStream, string path)
        {
            CreateDirectory();
            using var outFileStream = File.Create($"{imagesDir}{path}{format}");
            imageStream.CopyTo(outFileStream);
        }

        public static MemoryStream Load(string path)
        {
            var ms = new MemoryStream();
            using var inStream = File.OpenRead($"{imagesDir}{path}.png");
            inStream.CopyTo(ms);
            return ms;
        }

        public static Image LoadImage(string path)
        {
            return Image.FromFile($"{imagesDir}{path}.png");
        }

        public static bool Exists(string path)
        {
            return File.Exists($"{imagesDir}{path}.png");
        }

        public static void Delete(string path)
        {
            File.Delete($"{imagesDir}{path}.png");
        }
    }
}
