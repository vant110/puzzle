using System.Text.RegularExpressions;

namespace puzzle.Services
{
    static class Validator
    {
        private static readonly string loginPattern = @"^[A-Za-z\d]{4,8}$";
        private static readonly string passwordPattern = @"^[A-Za-z\d]{4,10}$";
        private static readonly string imageNamePattern = @"^[A-Za-zА-Яа-я\d \-]{3,30}$";
        private static readonly string levelNamePattern = @"^[A-Za-zА-Яа-я\d \,]{3,30}$";
        private static readonly string puzzleNamePattern = @"^[A-Za-zА-Яа-я\d \-\,]{3,30}$";

        public static bool IsLogin(string input)
        {
            return Regex.IsMatch(input, loginPattern);
        }

        public static bool IsPassword(string input)
        {
            return Regex.IsMatch(input, passwordPattern);
        }

        public static bool IsImageName(string input)
        {
            return Regex.IsMatch(input, imageNamePattern);
        }

        public static bool IsLevelName(string input)
        {
            return Regex.IsMatch(input, levelNamePattern);
        }

        public static bool IsPuzzleName(string input)
        {
            return Regex.IsMatch(input, puzzleNamePattern);
        }
    }
}
