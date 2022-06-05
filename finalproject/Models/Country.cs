using System;
using System.Collections.Generic;

namespace finalproject.Models
{
    public partial class Country
    {
        public Country()
        {
            Pops = new HashSet<Pop>();
        }

        public string Country1 { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Iso3 { get; set; } = null!;

        public virtual ICollection<Pop> Pops { get; set; }
    }
}
