using System;
using System.Collections.Generic;

#nullable disable

namespace Incident_Reporting.Data.Entities
{
    public partial class IncidentType
    {
        public IncidentType()
        {
            IncidentReports = new HashSet<IncidentReport>();
        }

        public int Id { get; set; }
        public string IncidentTypeName { get; set; }
        public string IncidentTypeDescription { get; set; }

        public virtual ICollection<IncidentReport> IncidentReports { get; set; }
    }
}
