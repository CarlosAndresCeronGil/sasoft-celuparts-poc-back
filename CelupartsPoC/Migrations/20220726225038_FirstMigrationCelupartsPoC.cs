using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelupartsPoC.Migrations
{
    public partial class FirstMigrationCelupartsPoC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersDto",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Names = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surnames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlternativePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDto", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    IdRequest = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(type: "int", nullable: true),
                    RequestType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickUpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickUpTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quote = table.Column<int>(type: "int", nullable: false),
                    StatusQuote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechnicalRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.IdRequest);
                    table.ForeignKey(
                        name: "FK_Request_UsersDto_IdUser",
                        column: x => x.IdUser,
                        principalTable: "UsersDto",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    IdEquipment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRequest = table.Column<int>(type: "int", nullable: true),
                    TypeOfEquipment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EquipmentBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelOrReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imei = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EquipmentInvoice = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.IdEquipment);
                    table.ForeignKey(
                        name: "FK_Equipment_Request_IdRequest",
                        column: x => x.IdRequest,
                        principalTable: "Request",
                        principalColumn: "IdRequest");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_IdRequest",
                table: "Equipment",
                column: "IdRequest");

            migrationBuilder.CreateIndex(
                name: "IX_Request_IdUser",
                table: "Request",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "UsersDto");
        }
    }
}
