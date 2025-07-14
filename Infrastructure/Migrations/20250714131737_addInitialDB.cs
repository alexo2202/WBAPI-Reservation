using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addInitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "TblVehicleTypes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblVehicleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblVehicles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: false),
                    BookingValuePerDay = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblVehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblVehicles_TblVehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalSchema: "dbo",
                        principalTable: "TblVehicleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "TblVehicleTypes",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 14, 13, 17, 37, 254, DateTimeKind.Utc).AddTicks(4915), "seed", null, null, "Sedán" },
                    { 2, new DateTime(2025, 7, 14, 13, 17, 37, 254, DateTimeKind.Utc).AddTicks(4920), "seed", null, null, "Coupé" },
                    { 3, new DateTime(2025, 7, 14, 13, 17, 37, 254, DateTimeKind.Utc).AddTicks(4922), "seed", null, null, "SUV" },
                    { 4, new DateTime(2025, 7, 14, 13, 17, 37, 254, DateTimeKind.Utc).AddTicks(4924), "seed", null, null, "Camioneta" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "TblVehicles",
                columns: new[] { "Id", "BookingValuePerDay", "Brand", "CreatedAt", "CreatedBy", "Model", "ModifiedAt", "ModifiedBy", "PlateNumber", "VehicleTypeId", "Year" },
                values: new object[,]
                {
                    { 1, 85000, "Renault", new DateTime(2025, 7, 14, 13, 17, 37, 254, DateTimeKind.Utc).AddTicks(5036), "seed", "Logan", null, null, "XXX000", 1, 2015 },
                    { 2, 150000, "Mazda", new DateTime(2025, 7, 14, 13, 17, 37, 254, DateTimeKind.Utc).AddTicks(5040), "seed", "CX-30", null, null, "AAA999", 3, 2022 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblVehicles_VehicleTypeId",
                schema: "dbo",
                table: "TblVehicles",
                column: "VehicleTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblVehicles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TblVehicleTypes",
                schema: "dbo");
        }
    }
}
