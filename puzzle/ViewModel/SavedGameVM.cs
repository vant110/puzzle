using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle.ViewModel
{
    public class SavedGameVM
    {
        public short SavedGameId { get; set; }
        public short PuzzleId { get; set; }
        public byte[] FieldFragmentNumbers { get; set; }
        public byte[] TapeFragmentNumbers { get; set; }
        public sbyte CountingMethodId { get; set; }
        public short Score { get; set; }
        public int Time { get; set; }
    }
}
