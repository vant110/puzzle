using System;
using System.Collections.Generic;

#nullable disable

namespace puzzle.Data
{
    public partial class SavedGame
    {
        public short SavedGameId { get; set; }
        public short PlayerId { get; set; }
        public short PuzzleId { get; set; }
        public byte[] FieldFragmentNumbers { get; set; }
        public sbyte CountingMethodId { get; set; }

        public virtual CountingMethod CountingMethod { get; set; }
        public virtual Player Player { get; set; }
        public virtual Puzzle Puzzle { get; set; }
        public virtual SavedGameScore SavedGameScore { get; set; }
        public virtual SavedGameTape SavedGameTape { get; set; }
        public virtual SavedGameTime SavedGameTime { get; set; }
    }
}
