using System;
using System.Collections.Generic;

#nullable disable

namespace puzzle.Data
{
    public partial class SavedGameTime
    {
        public short SavedGameId { get; set; }
        public int Time { get; set; }

        public virtual SavedGame SavedGame { get; set; }
    }
}
