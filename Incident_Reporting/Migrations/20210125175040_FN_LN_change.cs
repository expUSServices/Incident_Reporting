using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Incident_Reporting.Migrations
{
    public partial class FN_LN_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fileLocation = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false, comment: "The folder where the attachment is stored."),
                    fileExtension = table.Column<byte[]>(type: "varbinary(max)", nullable: false, comment: "The extension of the file, without the 'dot'. Normally 3 characters.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clientCompanyName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.id);
                },
                comment: "These are the 'report-to' companies. At the time of the creation of the DB, only TransCanada and Energy Transfer, apparently.");

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    countryName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    countryAbbrev = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Incident_Type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    incidentTypeName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    incidentTypeDescription = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incident_Type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Location_Class",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location_Class", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    lastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    projectName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    projectDescription = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    clientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.id);
                    table.ForeignKey(
                        name: "FK_Project_Client",
                        column: x => x.clientId,
                        principalTable: "Client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "State_Province",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stateProvinceName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    stateProvinceAbbrev = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    countryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State_Province", x => x.id);
                    table.ForeignKey(
                        name: "FK_StateProvince_Country",
                        column: x => x.countryId,
                        principalTable: "Country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Incident_Report",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    incidentTypeId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    projectId = table.Column<int>(type: "int", nullable: false),
                    LocationClassId = table.Column<int>(type: "int", nullable: false),
                    dateTimeIncidentUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    reporterCompanyName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    location = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    actionTaken = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    dateTimeReportedUtc = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incident_Report", x => x.id);
                    table.ForeignKey(
                        name: "FK_IncidentReport_IncidentType",
                        column: x => x.incidentTypeId,
                        principalTable: "Incident_Type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IncidentReport_Location_Class",
                        column: x => x.LocationClassId,
                        principalTable: "Location_Class",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IncidentReport_Project",
                        column: x => x.projectId,
                        principalTable: "Project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IncidentReport_User",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Incident_To_Attachment",
                columns: table => new
                {
                    incidentId = table.Column<int>(type: "int", nullable: false),
                    attachmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incident_To_Attachment", x => new { x.incidentId, x.attachmentId });
                    table.ForeignKey(
                        name: "FK_IncidentToAttachment_Attachment",
                        column: x => x.attachmentId,
                        principalTable: "Attachment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IncidentToAttachment_Incident",
                        column: x => x.incidentId,
                        principalTable: "Incident_Report",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Incident_To_StateProvince",
                columns: table => new
                {
                    incidentId = table.Column<int>(type: "int", nullable: false),
                    stateProvinceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incident_To_StateProvince", x => new { x.stateProvinceId, x.incidentId });
                    table.ForeignKey(
                        name: "FK_IncidentToStateProvince_Incident",
                        column: x => x.incidentId,
                        principalTable: "Incident_Report",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IncidentToStateProvince_StateProvince",
                        column: x => x.stateProvinceId,
                        principalTable: "State_Province",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "clientCompanyName_uniq",
                table: "Client",
                column: "clientCompanyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "countryAbbrev_uniq",
                table: "Country",
                column: "countryAbbrev",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "countryName_uniq",
                table: "Country",
                column: "countryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Country",
                table: "Country",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_Report_incidentTypeId",
                table: "Incident_Report",
                column: "incidentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_Report_LocationClassId",
                table: "Incident_Report",
                column: "LocationClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_Report_projectId",
                table: "Incident_Report",
                column: "projectId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_Report_userId",
                table: "Incident_Report",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_To_Attachment_attachmentId",
                table: "Incident_To_Attachment",
                column: "attachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_To_StateProvince_incidentId",
                table: "Incident_To_StateProvince",
                column: "incidentId");

            migrationBuilder.CreateIndex(
                name: "incidentTypeName_uniq",
                table: "Incident_Type",
                column: "incidentTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_clientId",
                table: "Project",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "projectName_uniq",
                table: "Project",
                column: "projectName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_State_Province_countryId",
                table: "State_Province",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "stateProvinceAbbrev_uniq",
                table: "State_Province",
                column: "stateProvinceAbbrev",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "stateProvinceName_uniq",
                table: "State_Province",
                column: "stateProvinceName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "email_uniq",
                table: "User",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incident_To_Attachment");

            migrationBuilder.DropTable(
                name: "Incident_To_StateProvince");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "Incident_Report");

            migrationBuilder.DropTable(
                name: "State_Province");

            migrationBuilder.DropTable(
                name: "Incident_Type");

            migrationBuilder.DropTable(
                name: "Location_Class");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
