using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoP2.Migrations.RegisterDB
{
    /// <inheritdoc />
    public partial class INITIALMIGRATIONCLINIC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentRegisters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PetId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateAppointment = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RemovedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentRegisters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwnerRegisters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CPF = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RemovedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerRegisters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PetRegisters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PetRG = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Color = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Specie = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Sex = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Castrated = table.Column<bool>(type: "boolean", nullable: false),
                    Community = table.Column<bool>(type: "boolean", nullable: false),
                    Microchipped = table.Column<bool>(type: "boolean", nullable: false),
                    MicrochippedNumber = table.Column<int>(type: "integer", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    State = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PhotoURL = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RemovedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetRegisters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PetRegisters_OwnerRegisters_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "OwnerRegisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OwnerRegisters_CPF",
                table: "OwnerRegisters",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OwnerRegisters_Email",
                table: "OwnerRegisters",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PetRegisters_OwnerId",
                table: "PetRegisters",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentRegisters");

            migrationBuilder.DropTable(
                name: "PetRegisters");

            migrationBuilder.DropTable(
                name: "OwnerRegisters");
        }
    }
}
