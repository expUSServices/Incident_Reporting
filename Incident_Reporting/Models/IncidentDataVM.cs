using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Incident_Reporting.Models
{
    public class IncidentDataVM
    {
        public int Id { get; set; }

        public int IncidentTypeId { get; set; }
        public string IncidentTypeName { get; set; }
        public string IncidentTypeDescription { get; set; }
        public int ClientId { get; set; }
        public string ClientCompanyName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int StateId { get; set; }
        public string StateProvinceName { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int ProjectId { get; set; }

        public int LocationId { get; set; }

        public DateTime? DateTimeIncidentUtc { get; set; }
        public string ReporterCompanyName { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string ActionTaken { get; set; }
        public DateTime DateTimeReportedUtc { get; set; }
    }
}
