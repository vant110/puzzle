using System.IO;

namespace puzzle.Model
{
    static class ImageDTO
    {
        private static string path;

        public static string Name { get; set; }
        public static string Path
        {
            get
            {
                return path;
            }
            set
            {
                if (value == null)
                {
                    path = value;
                    return;
                }
                int length = value.Length > 34 ? 34 : value.Length;
                path = value.Substring(0, length - 4);
            }
        }
        public static string Hash { get; set; }
        public static Stream Image { get; set; }
    }
}
