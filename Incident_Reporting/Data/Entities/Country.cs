using System;
using System.Collections.Generic;

#nullable disable

namespace Incident_Reporting.Data.Entities
{
    public partial class Country
    {
        public Country()
        {
            StateProvinces = new HashSet<StateProvince>();
        }

        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CountryAbbrev { get; set; }

        public virtual ICollection<StateProvince> StateProvinces { get; set; }
    }
}
