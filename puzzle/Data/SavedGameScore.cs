using System;
using System.Collections.Generic;

#nullable disable

namespace puzzle.Data
{
    public partial class SavedGameScore
    {
        public short SavedGameId { get; set; }
        public short Score { get; set; }

        public virtual SavedGame SavedGame { get; set; }
    }
}
