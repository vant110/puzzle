using System;
using System.Collections.Generic;

#nullable disable

namespace puzzle.Data
{
    public partial class FragmentType
    {
        public FragmentType()
        {
            DifficultyLevels = new HashSet<DifficultyLevel>();
        }

        public sbyte FragmentTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DifficultyLevel> DifficultyLevels { get; set; }
    }
}
