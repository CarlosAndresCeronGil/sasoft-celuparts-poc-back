using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelupartsPoC.Migrations
{
    public partial class NineTeenthMigrationsCelupartsPoC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Retoma",
                columns: table => new
                {
                    IdRetoma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRequest = table.Column<int>(type: "int", nullable: false),
                    RetomaQuote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceDiagnostic = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retoma", x => x.IdRetoma);
                    table.ForeignKey(
                        name: "FK_Retoma_Request_IdRequest",
                        column: x => x.IdRequest,
                        principalTable: "Request",
                        principalColumn: "IdRequest",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetomaPayment",
                columns: table => new
                {
                    IdRetomaPayment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRetoma = table.Column<int>(type: "int", nullable: false),
                    PaymentMehotd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetomaPayment", x => x.IdRetomaPayment);
                    table.ForeignKey(
                        name: "FK_RetomaPayment_Retoma_IdRetoma",
                        column: x => x.IdRetoma,
                        principalTable: "Retoma",
                        principalColumn: "IdRetoma",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Retoma_IdRequest",
                table: "Retoma",
                column: "IdRequest");

            migrationBuilder.CreateIndex(
                name: "IX_RetomaPayment_IdRetoma",
                table: "RetomaPayment",
                column: "IdRetoma");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RetomaPayment");

            migrationBuilder.DropTable(
                name: "Retoma");
        }
    }
}
