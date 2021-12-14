using System;
using System.Collections.Generic;

#nullable disable

namespace puzzle.Data
{
    public partial class SavedGameTape
    {
        public short SavedGameId { get; set; }
        public byte[] FragmentNumbers { get; set; }

        public virtual SavedGame SavedGame { get; set; }
    }
}
