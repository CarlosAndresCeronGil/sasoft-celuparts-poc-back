using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelupartsPoC.Migrations
{
    public partial class FiftyFifthMigrationCelupartsPoC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestHistory",
                columns: table => new
                {
                    IdRequestHistory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRequest = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestHistory", x => x.IdRequestHistory);
                    table.ForeignKey(
                        name: "FK_RequestHistory_Request_IdRequest",
                        column: x => x.IdRequest,
                        principalTable: "Request",
                        principalColumn: "IdRequest");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestHistory_IdRequest",
                table: "RequestHistory",
                column: "IdRequest");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestHistory");

            migrationBuilder.CreateTable(
                name: "RequestsHistory",
                columns: table => new
                {
                    IdRequestHistory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRequest = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestsHistory", x => x.IdRequestHistory);
                    table.ForeignKey(
                        name: "FK_RequestsHistory_Request_IdRequest",
                        column: x => x.IdRequest,
                        principalTable: "Request",
                        principalColumn: "IdRequest");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestsHistory_IdRequest",
                table: "RequestsHistory",
                column: "IdRequest");
        }
    }
}
