using System;
using Incident_Reporting.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Incident_Reporting.Data
{
    public partial class TCPL_Keystone_XL_Safety_ReportsContext : DbContext, ITCPL_Keystone_XL_Safety_ReportsContext
    {
        public TCPL_Keystone_XL_Safety_ReportsContext()
        {
        }

        public TCPL_Keystone_XL_Safety_ReportsContext(DbContextOptions<TCPL_Keystone_XL_Safety_ReportsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<IncidentReport> IncidentReports { get; set; }
        public virtual DbSet<IncidentToAttachment> IncidentToAttachments { get; set; }
        public virtual DbSet<IncidentToStateProvince> IncidentToStateProvinces { get; set; }
        public virtual DbSet<IncidentType> IncidentTypes { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<StateProvince> StateProvinces { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Location_Class> Location_Classes { get; set; }
        //DbSet<Attachment> ITCPL_Keystone_XL_Safety_ReportsContext.Attachments { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //DbSet<Client> ITCPL_Keystone_XL_Safety_ReportsContext.Clients { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //DbSet<Country> ITCPL_Keystone_XL_Safety_ReportsContext.Countries { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //DbSet<IncidentReport> ITCPL_Keystone_XL_Safety_ReportsContext.IncidentReports { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //DbSet<IncidentToAttachment> ITCPL_Keystone_XL_Safety_ReportsContext.IncidentToAttachments { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //DbSet<IncidentToStateProvince> ITCPL_Keystone_XL_Safety_ReportsContext.IncidentToStateProvinces { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //DbSet<IncidentType> ITCPL_Keystone_XL_Safety_ReportsContext.IncidentTypes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //DbSet<Project> ITCPL_Keystone_XL_Safety_ReportsContext.Projects { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //DbSet<StateProvince> ITCPL_Keystone_XL_Safety_ReportsContext.StateProvinces { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //DbSet<User> ITCPL_Keystone_XL_Safety_ReportsContext.Users { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //DbSet<Location_Class> ITCPL_Keystone_XL_Safety_ReportsContext.Location_Classes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DTALDBG001;Database=TCPL_Keystone_XL_Safety_Reports;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("Attachment");

                entity.Property(e => e.Id).HasColumnName("id");


                entity.Property(e => e.FileExtension)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("fileExtension")
                    .HasComment("The extension of the file, without the 'dot'. Normally 3 characters.");

                entity.Property(e => e.FileLocation)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("fileLocation")
                    .HasComment("The folder where the attachment is stored.");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.HasComment("These are the 'report-to' companies. At the time of the creation of the DB, only TransCanada and Energy Transfer, apparently.");

                entity.HasIndex(e => e.ClientCompanyName, "clientCompanyName_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClientCompanyName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("clientCompanyName");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.HasIndex(e => e.Id, "IX_Country");

                entity.HasIndex(e => e.CountryAbbrev, "countryAbbrev_uniq")
                    .IsUnique();

                entity.HasIndex(e => e.CountryName, "countryName_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryAbbrev)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("countryAbbrev")
                    .IsFixedLength(true);

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("countryName");
            });

            modelBuilder.Entity<IncidentReport>(entity =>
            {
                entity.ToTable("Incident_Report");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActionTaken)
                    .IsUnicode(false)
                    .HasColumnName("actionTaken");

                entity.Property(e => e.DateTimeIncident)
                    .HasColumnType("datetime")
                    .HasColumnName("dateTimeIncident");

                entity.Property(e => e.DateTimeReportSubmittedUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("dateTimeReportSubmittedUtc");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IncidentTypeId).HasColumnName("incidentTypeId");

                entity.Property(e => e.Location)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ProjectId).HasColumnName("projectId");
                entity.Property(e => e.StateProvinceId).HasColumnName("stateProvinceId");

                entity.Property(e => e.ReporterCompanyName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("reporterCompanyName");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.IncidentType)
                    .WithMany(p => p.IncidentReports)
                    .HasForeignKey(d => d.IncidentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IncidentReport_IncidentType");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.IncidentReports)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IncidentReport_Project");

                entity.HasOne(d => d.StateProvince)
                      .WithMany(p => p.IncidentReports)
                      .HasForeignKey(d => d.StateProvinceId)
                      .HasConstraintName("FK_Incident_Report_State_Province");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.IncidentReports)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IncidentReport_User");

                entity.HasOne(d => d.Location_Class)
                  .WithMany(p => p.IncidentReports)
                  .HasForeignKey(d => d.LocationClassId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_IncidentReport_Location_Class");
            });

            modelBuilder.Entity<IncidentToAttachment>(entity =>
            {
                entity.HasKey(e => new { e.IncidentId, e.AttachmentId });

                entity.ToTable("Incident_To_Attachment");

                entity.Property(e => e.IncidentId).HasColumnName("incidentId");

                entity.Property(e => e.AttachmentId).HasColumnName("attachmentId");

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.IncidentToAttachments)
                    .HasForeignKey(d => d.AttachmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IncidentToAttachment_Attachment");

                entity.HasOne(d => d.Incident)
                    .WithMany(p => p.IncidentToAttachments)
                    .HasForeignKey(d => d.IncidentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IncidentToAttachment_Incident");
            });

            modelBuilder.Entity<IncidentToStateProvince>(entity =>
            {
                entity.HasKey(e => new { e.StateProvinceId, e.IncidentId });

                entity.ToTable("Incident_To_StateProvince");

                entity.Property(e => e.StateProvinceId).HasColumnName("stateProvinceId");

                entity.Property(e => e.IncidentId).HasColumnName("incidentId");

                entity.HasOne(d => d.Incident)
                    .WithMany(p => p.IncidentToStateProvinces)
                    .HasForeignKey(d => d.IncidentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IncidentToStateProvince_Incident");

                entity.HasOne(d => d.StateProvince)
                    .WithMany(p => p.IncidentToStateProvinces)
                    .HasForeignKey(d => d.StateProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IncidentToStateProvince_StateProvince");
            });

            modelBuilder.Entity<IncidentType>(entity =>
            {
                entity.ToTable("Incident_Type");

                entity.HasIndex(e => e.IncidentTypeName, "incidentTypeName_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IncidentTypeDescription)
                    .IsUnicode(false)
                    .HasColumnName("incidentTypeDescription");

                entity.Property(e => e.IncidentTypeName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("incidentTypeName");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.HasIndex(e => e.ProjectName, "projectName_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClientId).HasColumnName("clientId");

                entity.Property(e => e.ProjectDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("projectDescription");

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("projectName");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Client");
            });

            modelBuilder.Entity<StateProvince>(entity =>
            {
                entity.ToTable("State_Province");

                entity.HasIndex(e => e.StateProvinceAbbrev, "stateProvinceAbbrev_uniq")
                    .IsUnique();

                entity.HasIndex(e => e.StateProvinceName, "stateProvinceName_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryId).HasColumnName("countryId");

                entity.Property(e => e.StateProvinceAbbrev)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("stateProvinceAbbrev")
                    .IsFixedLength(true);

                entity.Property(e => e.StateProvinceName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("stateProvinceName");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.StateProvinces)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StateProvince_Country");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "email_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                   // .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                  //  .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lastName");
            });
            modelBuilder.Entity<Location_Class>(entity =>
            {
                entity.ToTable("Location_Class");

            
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.locationClassName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
