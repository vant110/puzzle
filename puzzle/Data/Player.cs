using System;
using System.Collections.Generic;

#nullable disable

namespace puzzle.Data
{
    public partial class Player
    {
        public Player()
        {
            Records = new HashSet<Record>();
            SavedGames = new HashSet<SavedGame>();
        }

        public short PlayerId { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }

        public virtual ICollection<Record> Records { get; set; }
        public virtual ICollection<SavedGame> SavedGames { get; set; }
    }
}
