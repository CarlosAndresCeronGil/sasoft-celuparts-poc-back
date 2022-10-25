using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelupartsPoC.Migrations
{
    public partial class FiftyFourthMigrationCelupartsPoC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RepairStartDate",
                table: "Repair",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "RepairTime",
                table: "Repair",
                type: "time",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepairStartDate",
                table: "Repair");

            migrationBuilder.DropColumn(
                name: "RepairTime",
                table: "Repair");
        }
    }
}
