using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle.Model
{
    static class PuzzleDTO
    {
        public static string Name { get; set; }
        public static int ImageId { get; set; }
        public static int DifficultyLevelId { get; set; }
        public static byte[] FragmentNumbers { get; set; }
    }
}
