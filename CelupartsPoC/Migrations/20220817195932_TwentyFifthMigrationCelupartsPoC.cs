using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelupartsPoC.Migrations
{
    public partial class TwentyFifthMigrationCelupartsPoC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUserDto",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "IdUser",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_IdUser",
                table: "User",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_User_UsersDto_IdUser",
                table: "User",
                column: "IdUser",
                principalTable: "UsersDto",
                principalColumn: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_UsersDto_IdUser",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_IdUser",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "IdUserDto",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
