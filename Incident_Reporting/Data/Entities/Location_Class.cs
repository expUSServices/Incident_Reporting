using Incident_Reporting.Data.Entities;
using System.Collections.Generic;

namespace Incident_Reporting.Data.Entities
{
    public class Location_Class
    {
        public Location_Class()
        {
            IncidentReports = new HashSet<IncidentReport>();
        }
        public int Id { get; set; }
        public string locationClassName { get; set; }

        public virtual ICollection<IncidentReport> IncidentReports { get; set; }
    }
}