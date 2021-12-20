using System.IO;

namespace puzzle.Model
{
    static class NewImage
    {
        private static string path;
        
        public static string Name { get; set; }
        public static string Path {
            get
            {
                return path;
            }
            set
            {
                if (value == null)
                {
                    path = value;
                }
                else if (value.Length > 34)
                {
                    path = value.Substring(value.Length - 34, 30);
                }
                else
                {
                    path = value.Substring(0, value.Length - 4);
                }
            }
        }
        public static string Hash { get; set; }
        public static Stream Image { get; set; }
    }
}
