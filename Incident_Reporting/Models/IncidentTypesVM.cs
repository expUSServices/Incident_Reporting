using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Incident_Reporting.Models
{
    public class IncidentTypesVM
    {
        public int Id { get; set; }
        public string IncidentTypeName { get; set; }
        public string IncidentTypeDescription { get; set; }
    }
}
