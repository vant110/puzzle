using System;
using System.Collections.Generic;

#nullable disable

namespace puzzle.Data
{
    public partial class PuzzleField
    {
        public short PuzzleId { get; set; }
        public byte[] FragmentNumbers { get; set; }

        public virtual Puzzle Puzzle { get; set; }
    }
}
