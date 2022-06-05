using System;
using System.Collections.Generic;

namespace finalproject.Models
{
    public partial class Pop
    {
        public int Id { get; set; }
        public int year { get; set; }
        public long value { get; set; }
        public int Countryid { get; set; }

        public virtual Countries Country { get; set; } = null!;
    }
}
