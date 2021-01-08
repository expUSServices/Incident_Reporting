using Incident_Reporting.Data.Entities;
using System.Collections.Generic;

namespace Incident_Reporting.Data.Entities
{
    public class Location_Class
    {
        public Location_Class()
        {
            Projects = new HashSet<Project>();
        }
        public int Id { get; set; }
        public string locationClassName { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}