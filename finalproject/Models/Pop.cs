using System;
using System.Collections.Generic;

namespace finalproject.Models
{
    public partial class Pop
    {
        public int year { get; set; }
        public long value { get; set; }
        public string Country { get; set; } = null!;
        public int Id { get; set; }

        public virtual Countries CountryNavigation { get; set; } = null!;
    }
}
