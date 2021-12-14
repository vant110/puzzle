using System;
using System.Collections.Generic;

#nullable disable

namespace puzzle.Data
{
    public partial class AssemblyType
    {
        public AssemblyType()
        {
            DifficultyLevels = new HashSet<DifficultyLevel>();
        }

        public sbyte AssemblyTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DifficultyLevel> DifficultyLevels { get; set; }
    }
}
