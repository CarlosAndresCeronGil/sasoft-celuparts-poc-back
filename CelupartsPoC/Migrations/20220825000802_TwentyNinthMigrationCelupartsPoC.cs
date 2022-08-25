using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelupartsPoC.Migrations
{
    public partial class TwentyNinthMigrationCelupartsPoC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestNotification",
                columns: table => new
                {
                    IdRequestNotification = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRequest = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HideNotification = table.Column<bool>(type: "bit", nullable: false),
                    NotificationType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestNotification", x => x.IdRequestNotification);
                    table.ForeignKey(
                        name: "FK_RequestNotification_Request_IdRequest",
                        column: x => x.IdRequest,
                        principalTable: "Request",
                        principalColumn: "IdRequest");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestNotification_IdRequest",
                table: "RequestNotification",
                column: "IdRequest");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestNotification");
        }
    }
}
