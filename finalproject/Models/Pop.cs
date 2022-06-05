using System;
using System.Collections.Generic;

namespace finalproject.Models
{
    public partial class Pop
    {
        public int Year { get; set; }
        public long Value { get; set; }
        public string Country { get; set; } = null!;

        public virtual Country CountryNavigation { get; set; } = null!;
    }
}
