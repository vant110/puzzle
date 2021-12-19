using System;
using System.Collections.Generic;

#nullable disable

namespace puzzle.Data
{
    public partial class CountingMethod
    {
        public CountingMethod()
        {
            Records = new HashSet<Record>();
            SavedGames = new HashSet<SavedGame>();
        }

        public sbyte CountingMethodId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Record> Records { get; set; }
        public virtual ICollection<SavedGame> SavedGames { get; set; }
    }
}
