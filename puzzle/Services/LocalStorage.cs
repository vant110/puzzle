using puzzle.Model;
using System.IO;
using System.Windows.Forms;

namespace puzzle.Services
{
    static class LocalStorage
    {
        private static string imagesPath = Application.StartupPath + "images";

        private static void CreateDirectory()
        {
            var directoryInfo = new DirectoryInfo(imagesPath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }

        public static void SaveNewImage()
        {
            CreateDirectory();
            using (var fileStream = File.Create($"{imagesPath}\\{NewImage.Path}.png"))
            {
                NewImage.Image.CopyTo(fileStream);
            }
            NewImage.Image.Seek(0, SeekOrigin.Begin);
        }
    }
}
