using System;
using System.Collections.Generic;

#nullable disable

namespace Incident_Reporting.Data.Entities
{
    public partial class User
    {
        public User()
        {
            IncidentReports = new HashSet<IncidentReport>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public virtual ICollection<IncidentReport> IncidentReports { get; set; }
    }
}
