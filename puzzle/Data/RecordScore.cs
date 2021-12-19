using System;
using System.Collections.Generic;

#nullable disable

namespace puzzle.Data
{
    public partial class RecordScore
    {
        public short RecordId { get; set; }
        public short Score { get; set; }

        public virtual Record Record { get; set; }
    }
}
