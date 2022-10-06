using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelupartsPoC.Migrations
{
    public partial class FiftySecondMigrationCelupartsPoC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdUserDto",
                table: "User",
                newName: "IdUser");

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

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "User",
                newName: "IdUserDto");
        }
    }
}
