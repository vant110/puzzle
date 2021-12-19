using System;
using System.Collections.Generic;

#nullable disable

namespace puzzle.Data
{
    public partial class Record
    {
        public short RecordId { get; set; }
        public short PuzzleId { get; set; }
        public sbyte CountingMethodId { get; set; }
        public short PlayerId { get; set; }

        public virtual CountingMethod CountingMethod { get; set; }
        public virtual Player Player { get; set; }
        public virtual Puzzle Puzzle { get; set; }
        public virtual RecordScore RecordScore { get; set; }
        public virtual RecordTime RecordTime { get; set; }
    }
}
