using System;
using System.Collections.Generic;

#nullable disable

namespace Incident_Reporting.Data.Entities
{
    public partial class Project
    {
        public Project()
        {
            IncidentReports = new HashSet<IncidentReport>();
        }

        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<IncidentReport> IncidentReports { get; set; }
    }
}
