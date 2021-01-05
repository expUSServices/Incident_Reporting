using System;
using System.Collections.Generic;

#nullable disable

namespace Incident_Reporting.Data.Entities
{
    public partial class IncidentToStateProvince
    {
        public int IncidentId { get; set; }
        public int StateProvinceId { get; set; }

        public virtual IncidentReport Incident { get; set; }
        public virtual StateProvince StateProvince { get; set; }
    }
}
