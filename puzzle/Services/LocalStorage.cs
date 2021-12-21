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
        /*
        public static MemoryStream ZoomImage(Stream imageStream)
        {
            float percent = 200;
            using Image orig = Image.FromStream(imageStream);
            Bitmap scaledImage;
            /// Ширина и высота результирующего изображения
            float w = orig.Width * percent / 100,
                h = orig.Height * percent / 100;
            w = 600;
            h = 450;
            scaledImage = new Bitmap((int)w, (int)h);
            /// DPI результирующего изображения
            scaledImage.SetResolution(orig.HorizontalResolution, orig.VerticalResolution);
            /// Часть исходного изображения, для которой меняем масштаб.
            /// В данном случае — всё изображение
            Rectangle src = new(0, 0, orig.Width, orig.Height);
            /// Часть изображения, которую будем рисовать
            /// В данном случае — всё изображение
            Rectangle dest = new(0, 0, (int)w, (int)h);
            /// Прорисовка с изменённым масштабом
            using (Graphics g = Graphics.FromImage(scaledImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(orig, dest, src, GraphicsUnit.Pixel);
            }
            MemoryStream ms = new();
            scaledImage.Save(ms, ImageFormat.Png);
            return ms;
        }
        */
        public static MemoryStream ResizeImage(Stream imageStream)
        {
            using Bitmap bitmap = new(600, 450);
            using (Image image = Image.FromStream(imageStream))
            {
                imageStream.Close();
                 
                int x0 = 0;
                int y0 = 0;
                int x1 = bitmap.Width;
                int y1 = bitmap.Height;
                if (image.Height < y1
                    || image.Width < x1)
                {
                    double k1 = (double)y1 / image.Height;
                    double k2 = (double)x1 / image.Width;
                    double k = k1 > k2 ? k1 : k2;
                    int d1 = (int)((image.Width * k - bitmap.Width) * k / 2);
                    x0 -= d1 / 2;
                    x1 += d1;
                    int d2 = (int)((image.Height * k - bitmap.Height) * k / 2);
                    y0 -= d2 / 2;
                    y1 += d2;
                }
                else
                {
                    double k1 = (double)image.Height / y1;
                    double k2 = (double)image.Width / x1;
                    double k = k1 < k2 ? k1 : k2;
                    int d1 = (int)((image.Width / k - bitmap.Width) * k / 2);
                    x0 -= d1 / 2;
                    x1 += d1;
                    int d2 = (int)((image.Height / k - bitmap.Height) * k / 2);
                    y0 -= d2 / 2;
                    y1 += d2;
                }
                Rectangle destRect = new(
                    x0, y0, x1, y1);
                Rectangle srcRect = new(
                    0, 0,
                    image.Width, image.Height);
                using Graphics graphics = Graphics.FromImage(bitmap);
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
