using System.IO;

namespace puzzle.Model
{
    static class NewImage
    {
        public static string Name { get; set; }
        public static string Path { get; set; }
        public static string Hash { get; set; }
        public static Stream Image { get; set; }
    }
}
