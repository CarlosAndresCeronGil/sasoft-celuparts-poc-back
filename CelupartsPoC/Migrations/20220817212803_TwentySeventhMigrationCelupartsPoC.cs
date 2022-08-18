using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelupartsPoC.Migrations
{
    public partial class TwentySeventhMigrationCelupartsPoC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdTechnician",
                table: "Retoma",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Retoma_IdTechnician",
                table: "Retoma",
                column: "IdTechnician");

            migrationBuilder.AddForeignKey(
                name: "FK_Retoma_Technician_IdTechnician",
                table: "Retoma",
                column: "IdTechnician",
                principalTable: "Technician",
                principalColumn: "IdTechnician");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Retoma_Technician_IdTechnician",
                table: "Retoma");

            migrationBuilder.DropIndex(
                name: "IX_Retoma_IdTechnician",
                table: "Retoma");

            migrationBuilder.DropColumn(
                name: "IdTechnician",
                table: "Retoma");
        }
    }
}
