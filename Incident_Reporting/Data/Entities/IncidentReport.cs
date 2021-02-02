using System;
using System.Collections.Generic;

#nullable disable

namespace Incident_Reporting.Data.Entities
{
    public partial class IncidentReport
    {
        public IncidentReport()
        {
            IncidentToAttachments = new HashSet<IncidentToAttachment>();
            IncidentToStateProvinces = new HashSet<IncidentToStateProvince>();
        }

        public int Id { get; set; }
        public int IncidentTypeId { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int StateProvinceId { get; set; }
        public int LocationClassId { get; set; }
        public DateTime? DateTimeIncident { get; set; }
        public string ReporterCompanyName { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string ActionTaken { get; set; }
        public DateTime DateTimeReportSubmittedUtc { get; set; }
        //ReportSubmitted
        public virtual IncidentType IncidentType { get; set; }
        public virtual Project Project { get; set; }
        public virtual User User { get; set; }

        public virtual StateProvince StateProvince { get; set; }
        public virtual Location_Class Location_Class { get; set; }
        public virtual ICollection<IncidentToAttachment> IncidentToAttachments { get; set; }
        public virtual ICollection<IncidentToStateProvince> IncidentToStateProvinces { get; set; }
    }
}
