using puzzle.Model;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace puzzle.Services
{
    static class LocalStorage
    {
        private static string imagesPath = Application.StartupPath + "images\\";

        private static void CreateDirectory()
        {
            var directoryInfo = new DirectoryInfo(imagesPath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }

        public static void SaveAndCloseNewImage()
        {
            CreateDirectory();
            using (var fileStream = File.Create($"{imagesPath}{NewImage.Path}.png"))
            {
                NewImage.Image.CopyTo(fileStream);
            }
            NewImage.Image.Close();
        }
              
        public static Stream Load(string path)
        {
            return File.OpenRead($"{imagesPath}{path}.png");
        }

        public static Image LoadImage(string path)
        {
            return Image.FromFile($"{imagesPath}{path}.png");
        }

        public static bool Exists(string path)
        {
            return File.Exists($"{imagesPath}{path}.png");
        }

        public static void Delete(string path)
        {
            File.Delete($"{imagesPath}{path}.png");
        }
    }
}
