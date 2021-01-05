using System;
using System.Collections.Generic;

#nullable disable

namespace Incident_Reporting.Data.Entities
{
    public partial class StateProvince
    {
        public StateProvince()
        {
            IncidentToStateProvinces = new HashSet<IncidentToStateProvince>();
        }

        public int Id { get; set; }
        public string StateProvinceName { get; set; }
        public string StateProvinceAbbrev { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<IncidentToStateProvince> IncidentToStateProvinces { get; set; }
    }
}
