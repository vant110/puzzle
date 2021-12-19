using System;
using System.Collections.Generic;

#nullable disable

namespace puzzle.Data
{
    public partial class Puzzle
    {
        public Puzzle()
        {
            Records = new HashSet<Record>();
            SavedGames = new HashSet<SavedGame>();
        }

        public short PuzzleId { get; set; }
        public string Name { get; set; }
        public short ImageId { get; set; }
        public sbyte DifficultyLevelId { get; set; }

        public virtual DifficultyLevel DifficultyLevel { get; set; }
        public virtual Gallery Image { get; set; }
        public virtual PuzzleField PuzzleField { get; set; }
        public virtual PuzzleTape PuzzleTape { get; set; }
        public virtual ICollection<Record> Records { get; set; }
        public virtual ICollection<SavedGame> SavedGames { get; set; }
    }
}
