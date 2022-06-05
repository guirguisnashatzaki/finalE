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

        public int Id { get; set; }
        public string country { get; set; } = null!;
        public string? code { get; set; }
        public string? Iso3 { get; set; }

        public virtual ICollection<Pop> populationCounts { get; set; }
    }
}
