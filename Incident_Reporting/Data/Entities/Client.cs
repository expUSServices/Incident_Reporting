using System;
using System.Collections.Generic;

#nullable disable

namespace Incident_Reporting.Data.Entities
{
    public partial class Client
    {
        public Client()
        {
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string ClientCompanyName { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
