using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoP2.Migrations
{
    /// <inheritdoc />
    public partial class INITIALMIGRATIONCLINIC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VetClinics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CPF = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    CRMV = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RemovedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VetClinics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentClinics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VetId = table.Column<Guid>(type: "uuid", nullable: false),
                    PetId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateAppointment = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RemovedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentClinics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentClinics_VetClinics_VetId",
                        column: x => x.VetId,
                        principalTable: "VetClinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentClinics_VetId",
                table: "AppointmentClinics",
                column: "VetId");

            migrationBuilder.CreateIndex(
                name: "IX_VetClinics_CPF",
                table: "VetClinics",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VetClinics_CRMV",
                table: "VetClinics",
                column: "CRMV",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VetClinics_Email",
                table: "VetClinics",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentClinics");

            migrationBuilder.DropTable(
                name: "VetClinics");
        }
    }
}
