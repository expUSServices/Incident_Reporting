using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Incident_Reporting.Models
{
    public class IncidentDataVM
    {
        public int Id { get; set; }
        [Required]
        public int IncidentTypeId { get; set; }
      
        public string IncidentTypeName { get; set; }
        public string IncidentTypeDescription { get; set; }
        [Required]
        public int ClientId { get; set; }
        public string ClientCompanyName { get; set; }
        [Required]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        [Required]
        public int StateId { get; set; }
        public string StateProvinceName { get; set; }
        [Required]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public int ProjectId { get; set; }
        [Required]
        public int LocationId { get; set; }

        public string TimeZone { get; set; }
        [Required]
        public DateTime? DateTimeIncidentUtc { get; set; }
        public string ReporterCompanyName { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string ActionTaken { get; set; }
        public DateTime DateTimeReportedUtc { get; set; }

        public int AttachId { get; set; }
        public string FileLocation { get; set; }
        public byte[] FileExtension { get; set; }
    }
}
