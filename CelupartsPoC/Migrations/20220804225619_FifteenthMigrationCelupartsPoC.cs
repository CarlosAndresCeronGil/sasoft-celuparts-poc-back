using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelupartsPoC.Migrations
{
    public partial class FifteenthMigrationCelupartsPoC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdRequest",
                table: "Equipment");

            migrationBuilder.RenameColumn(
                name: "surnames",
                table: "Technician",
                newName: "Surnames");

            migrationBuilder.AddColumn<string>(
                name: "AccountStatus",
                table: "Courier",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountStatus",
                table: "Courier");

            migrationBuilder.RenameColumn(
                name: "Surnames",
                table: "Technician",
                newName: "surnames");

            migrationBuilder.AddColumn<int>(
                name: "IdRequest",
                table: "Equipment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
