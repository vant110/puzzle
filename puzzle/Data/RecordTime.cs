using System;
using System.Collections.Generic;

#nullable disable

namespace puzzle.Data
{
    public partial class RecordTime
    {
        public short RecordId { get; set; }
        public int Time { get; set; }

        public virtual Record Record { get; set; }
    }
}
