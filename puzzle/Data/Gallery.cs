using System;
using System.Collections.Generic;

#nullable disable

namespace puzzle.Data
{
    public partial class Gallery
    {
        public Gallery()
        {
            Puzzles = new HashSet<Puzzle>();
        }

        public short ImageId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string ImageHash { get; set; }

        public virtual ICollection<Puzzle> Puzzles { get; set; }
    }
}
