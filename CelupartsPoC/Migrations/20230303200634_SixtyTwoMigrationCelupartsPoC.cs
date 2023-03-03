using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelupartsPoC.Migrations
{
    public partial class SixtyTwoMigrationCelupartsPoC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RepairDiagnostic",
                table: "Repair",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepairDiagnostic",
                table: "Repair");
        }
    }
}
