using Incident_Reporting.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Incident_Reporting.Data
{
    public interface ITCPL_Keystone_XL_Safety_ReportsContext
    {
        DbSet<Attachment> Attachments { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<IncidentReport> IncidentReports { get; set; }
        DbSet<IncidentToAttachment> IncidentToAttachments { get; set; }
        DbSet<IncidentToStateProvince> IncidentToStateProvinces { get; set; }
        DbSet<IncidentType> IncidentTypes { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<StateProvince> StateProvinces { get; set; }
        DbSet<User> Users { get; set; }
    }
}
