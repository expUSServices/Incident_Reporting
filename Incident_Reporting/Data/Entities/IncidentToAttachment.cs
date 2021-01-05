using System;
using System.Collections.Generic;

#nullable disable

namespace Incident_Reporting.Data.Entities
{
    public partial class IncidentToAttachment
    {
        public int IncidentId { get; set; }
        public int AttachmentId { get; set; }

        public virtual Attachment Attachment { get; set; }
        public virtual IncidentReport Incident { get; set; }
    }
}
