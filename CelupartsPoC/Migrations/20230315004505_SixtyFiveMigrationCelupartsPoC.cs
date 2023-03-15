using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelupartsPoC.Migrations
{
    public partial class SixtyFiveMigrationCelupartsPoC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersDto_IdNumber",
                table: "UsersDto");

            migrationBuilder.AlterColumn<string>(
                name: "IdNumber",
                table: "UsersDto",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_UsersDto_IdNumber",
                table: "UsersDto",
                column: "IdNumber",
                unique: true,
                filter: "[IdNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersDto_IdNumber",
                table: "UsersDto");

            migrationBuilder.AlterColumn<string>(
                name: "IdNumber",
                table: "UsersDto",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersDto_IdNumber",
                table: "UsersDto",
                column: "IdNumber",
                unique: true);
        }
    }
}
