using System;
using System.Collections.Generic;

namespace finalproject.Models
{
    public partial class Countries
    {
        public Countries()
        {
            populationCounts = new HashSet<Pop>();
        }

        public string country { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Iso3 { get; set; } = null!;

        public virtual ICollection<Pop> populationCounts { get; set; }
    }
}
