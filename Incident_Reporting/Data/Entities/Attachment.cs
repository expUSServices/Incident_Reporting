using System;
using System.Collections.Generic;

#nullable disable

namespace Incident_Reporting.Data.Entities
{
    public partial class Attachment
    {
        public Attachment()
        {
            IncidentToAttachments = new HashSet<IncidentToAttachment>();
        }

        public int Id { get; set; }
        public string FileLocation { get; set; }
        public string FileExtension { get; set; }


        public virtual ICollection<IncidentToAttachment> IncidentToAttachments { get; set; }
    }
}
