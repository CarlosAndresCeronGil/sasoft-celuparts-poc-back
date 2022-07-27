using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelupartsPoC.Migrations
{
    public partial class FifthMigrationsCelupartsPoC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Request_IdRequest",
                table: "Equipment");

            migrationBuilder.AlterColumn<int>(
                name: "IdRequest",
                table: "Equipment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Request_IdRequest",
                table: "Equipment",
                column: "IdRequest",
                principalTable: "Request",
                principalColumn: "IdRequest",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Request_IdRequest",
                table: "Equipment");

            migrationBuilder.AlterColumn<int>(
                name: "IdRequest",
                table: "Equipment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Request_IdRequest",
                table: "Equipment",
                column: "IdRequest",
                principalTable: "Request",
                principalColumn: "IdRequest");
        }
    }
}
