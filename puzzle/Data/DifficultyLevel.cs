using System;
using System.Collections.Generic;

#nullable disable

namespace puzzle.Data
{
    public partial class DifficultyLevel
    {
        public DifficultyLevel()
        {
            Puzzles = new HashSet<Puzzle>();
        }

        public sbyte DifficultyLevelId { get; set; }
        public string Name { get; set; }
        public sbyte HorizontalFragmentCount { get; set; }
        public sbyte VerticalFragmentCount { get; set; }
        public sbyte FragmentTypeId { get; set; }
        public sbyte AssemblyTypeId { get; set; }

        public virtual AssemblyType AssemblyType { get; set; }
        public virtual FragmentType FragmentType { get; set; }
        public virtual ICollection<Puzzle> Puzzles { get; set; }
    }
}
