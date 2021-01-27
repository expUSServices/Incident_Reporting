﻿// <auto-generated />
using System;
using Incident_Reporting.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Incident_Reporting.Migrations
{
    [DbContext(typeof(TCPL_Keystone_XL_Safety_ReportsContext))]
    partial class TCPL_Keystone_XL_Safety_ReportsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Incident_Reporting.Data.Entities.Attachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .UseIdentityColumn();

                    b.Property<byte[]>("FileExtension")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("fileExtension")
                        .HasComment("The extension of the file, without the 'dot'. Normally 3 characters.");

                    b.Property<string>("FileLocation")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("fileLocation")
                        .HasComment("The folder where the attachment is stored.");

                    b.HasKey("Id");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .UseIdentityColumn();

                    b.Property<string>("ClientCompanyName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("clientCompanyName");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ClientCompanyName" }, "clientCompanyName_uniq")
                        .IsUnique();

                    b.ToTable("Client");

                    b
                        .HasComment("These are the 'report-to' companies. At the time of the creation of the DB, only TransCanada and Energy Transfer, apparently.");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .UseIdentityColumn();

                    b.Property<string>("CountryAbbrev")
                        .IsRequired()
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("char(3)")
                        .HasColumnName("countryAbbrev")
                        .IsFixedLength(true);

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("countryName");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "IX_Country");

                    b.HasIndex(new[] { "CountryAbbrev" }, "countryAbbrev_uniq")
                        .IsUnique();

                    b.HasIndex(new[] { "CountryName" }, "countryName_uniq")
                        .IsUnique();

                    b.ToTable("Country");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.IncidentReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .UseIdentityColumn();

                    b.Property<string>("ActionTaken")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("actionTaken");

                    b.Property<DateTime?>("DateTimeIncidentUtc")
                        .HasColumnType("datetime")
                        .HasColumnName("dateTimeIncidentUtc");

                    b.Property<DateTime>("DateTimeReportedUtc")
                        .HasColumnType("datetime")
                        .HasColumnName("dateTimeReportedUtc");

                    b.Property<string>("Description")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("description");

                    b.Property<int>("IncidentTypeId")
                        .HasColumnType("int")
                        .HasColumnName("incidentTypeId");

                    b.Property<string>("Location")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("location");

                    b.Property<int>("LocationClassId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("projectId");

                    b.Property<string>("ReporterCompanyName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("reporterCompanyName");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("userId");

                    b.HasKey("Id");

                    b.HasIndex("IncidentTypeId");

                    b.HasIndex("LocationClassId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Incident_Report");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.IncidentToAttachment", b =>
                {
                    b.Property<int>("IncidentId")
                        .HasColumnType("int")
                        .HasColumnName("incidentId");

                    b.Property<int>("AttachmentId")
                        .HasColumnType("int")
                        .HasColumnName("attachmentId");

                    b.HasKey("IncidentId", "AttachmentId");

                    b.HasIndex("AttachmentId");

                    b.ToTable("Incident_To_Attachment");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.IncidentToStateProvince", b =>
                {
                    b.Property<int>("StateProvinceId")
                        .HasColumnType("int")
                        .HasColumnName("stateProvinceId");

                    b.Property<int>("IncidentId")
                        .HasColumnType("int")
                        .HasColumnName("incidentId");

                    b.HasKey("StateProvinceId", "IncidentId");

                    b.HasIndex("IncidentId");

                    b.ToTable("Incident_To_StateProvince");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.IncidentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .UseIdentityColumn();

                    b.Property<string>("IncidentTypeDescription")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("incidentTypeDescription");

                    b.Property<string>("IncidentTypeName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("incidentTypeName");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "IncidentTypeName" }, "incidentTypeName_uniq")
                        .IsUnique();

                    b.ToTable("Incident_Type");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.Location_Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .UseIdentityColumn();

                    b.Property<string>("locationClassName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("firstName");

                    b.HasKey("Id");

                    b.ToTable("Location_Class");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .UseIdentityColumn();

                    b.Property<int>("ClientId")
                        .HasColumnType("int")
                        .HasColumnName("clientId");

                    b.Property<string>("ProjectDescription")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("projectDescription");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("projectName");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex(new[] { "ProjectName" }, "projectName_uniq")
                        .IsUnique();

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.StateProvince", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .UseIdentityColumn();

                    b.Property<int>("CountryId")
                        .HasColumnType("int")
                        .HasColumnName("countryId");

                    b.Property<string>("StateProvinceAbbrev")
                        .IsRequired()
                        .HasMaxLength(2)
                        .IsUnicode(false)
                        .HasColumnType("char(2)")
                        .HasColumnName("stateProvinceAbbrev")
                        .IsFixedLength(true);

                    b.Property<string>("StateProvinceName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("stateProvinceName");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex(new[] { "StateProvinceAbbrev" }, "stateProvinceAbbrev_uniq")
                        .IsUnique();

                    b.HasIndex(new[] { "StateProvinceName" }, "stateProvinceName_uniq")
                        .IsUnique();

                    b.ToTable("State_Province");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("firstName");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("lastName");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Email" }, "email_uniq")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.IncidentReport", b =>
                {
                    b.HasOne("Incident_Reporting.Data.Entities.IncidentType", "IncidentType")
                        .WithMany("IncidentReports")
                        .HasForeignKey("IncidentTypeId")
                        .HasConstraintName("FK_IncidentReport_IncidentType")
                        .IsRequired();

                    b.HasOne("Incident_Reporting.Data.Entities.Location_Class", "Location_Class")
                        .WithMany("IncidentReports")
                        .HasForeignKey("LocationClassId")
                        .HasConstraintName("FK_IncidentReport_Location_Class")
                        .IsRequired();

                    b.HasOne("Incident_Reporting.Data.Entities.Project", "Project")
                        .WithMany("IncidentReports")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK_IncidentReport_Project")
                        .IsRequired();

                    b.HasOne("Incident_Reporting.Data.Entities.User", "User")
                        .WithMany("IncidentReports")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_IncidentReport_User")
                        .IsRequired();

                    b.Navigation("IncidentType");

                    b.Navigation("Location_Class");

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.IncidentToAttachment", b =>
                {
                    b.HasOne("Incident_Reporting.Data.Entities.Attachment", "Attachment")
                        .WithMany("IncidentToAttachments")
                        .HasForeignKey("AttachmentId")
                        .HasConstraintName("FK_IncidentToAttachment_Attachment")
                        .IsRequired();

                    b.HasOne("Incident_Reporting.Data.Entities.IncidentReport", "Incident")
                        .WithMany("IncidentToAttachments")
                        .HasForeignKey("IncidentId")
                        .HasConstraintName("FK_IncidentToAttachment_Incident")
                        .IsRequired();

                    b.Navigation("Attachment");

                    b.Navigation("Incident");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.IncidentToStateProvince", b =>
                {
                    b.HasOne("Incident_Reporting.Data.Entities.IncidentReport", "Incident")
                        .WithMany("IncidentToStateProvinces")
                        .HasForeignKey("IncidentId")
                        .HasConstraintName("FK_IncidentToStateProvince_Incident")
                        .IsRequired();

                    b.HasOne("Incident_Reporting.Data.Entities.StateProvince", "StateProvince")
                        .WithMany("IncidentToStateProvinces")
                        .HasForeignKey("StateProvinceId")
                        .HasConstraintName("FK_IncidentToStateProvince_StateProvince")
                        .IsRequired();

                    b.Navigation("Incident");

                    b.Navigation("StateProvince");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.Project", b =>
                {
                    b.HasOne("Incident_Reporting.Data.Entities.Client", "Client")
                        .WithMany("Projects")
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK_Project_Client")
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.StateProvince", b =>
                {
                    b.HasOne("Incident_Reporting.Data.Entities.Country", "Country")
                        .WithMany("StateProvinces")
                        .HasForeignKey("CountryId")
                        .HasConstraintName("FK_StateProvince_Country")
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.Attachment", b =>
                {
                    b.Navigation("IncidentToAttachments");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.Client", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.Country", b =>
                {
                    b.Navigation("StateProvinces");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.IncidentReport", b =>
                {
                    b.Navigation("IncidentToAttachments");

                    b.Navigation("IncidentToStateProvinces");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.IncidentType", b =>
                {
                    b.Navigation("IncidentReports");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.Location_Class", b =>
                {
                    b.Navigation("IncidentReports");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.Project", b =>
                {
                    b.Navigation("IncidentReports");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.StateProvince", b =>
                {
                    b.Navigation("IncidentToStateProvinces");
                });

            modelBuilder.Entity("Incident_Reporting.Data.Entities.User", b =>
                {
                    b.Navigation("IncidentReports");
                });
#pragma warning restore 612, 618
        }
    }
}
