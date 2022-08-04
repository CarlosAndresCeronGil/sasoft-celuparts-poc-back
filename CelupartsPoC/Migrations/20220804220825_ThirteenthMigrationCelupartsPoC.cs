using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelupartsPoC.Migrations
{
    public partial class ThirteenthMigrationCelupartsPoC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Request_IdRequest",
                table: "Equipment");

            migrationBuilder.DropTable(
                name: "ProductReview");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_IdRequest",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "PickUpTime",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Quote",
                table: "Request");

            migrationBuilder.AddColumn<int>(
                name: "IdEquipment",
                table: "Request",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestIdRequest",
                table: "Equipment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Courier",
                columns: table => new
                {
                    IdCourier = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Names = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surnames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courier", x => x.IdCourier);
                });

            migrationBuilder.CreateTable(
                name: "Technician",
                columns: table => new
                {
                    IdTechnician = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Names = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    surnames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technician", x => x.IdTechnician);
                });

            migrationBuilder.CreateTable(
                name: "HomeService",
                columns: table => new
                {
                    IdHomeService = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRequest = table.Column<int>(type: "int", nullable: true),
                    IdCourier = table.Column<int>(type: "int", nullable: true),
                    PickUpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeService", x => x.IdHomeService);
                    table.ForeignKey(
                        name: "FK_HomeService_Courier_IdCourier",
                        column: x => x.IdCourier,
                        principalTable: "Courier",
                        principalColumn: "IdCourier");
                    table.ForeignKey(
                        name: "FK_HomeService_Request_IdRequest",
                        column: x => x.IdRequest,
                        principalTable: "Request",
                        principalColumn: "IdRequest");
                });

            migrationBuilder.CreateTable(
                name: "Repair",
                columns: table => new
                {
                    IdRepair = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRequest = table.Column<int>(type: "int", nullable: false),
                    IdTechnician = table.Column<int>(type: "int", nullable: false),
                    RepairDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeviceDiagnostic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepairQuote = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repair", x => x.IdRepair);
                    table.ForeignKey(
                        name: "FK_Repair_Request_IdRequest",
                        column: x => x.IdRequest,
                        principalTable: "Request",
                        principalColumn: "IdRequest",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Repair_Technician_IdTechnician",
                        column: x => x.IdTechnician,
                        principalTable: "Technician",
                        principalColumn: "IdTechnician",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairPayment",
                columns: table => new
                {
                    IdRepairPayment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRepair = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillPayment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairPayment", x => x.IdRepairPayment);
                    table.ForeignKey(
                        name: "FK_RepairPayment_Repair_IdRepair",
                        column: x => x.IdRepair,
                        principalTable: "Repair",
                        principalColumn: "IdRepair",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Request_IdEquipment",
                table: "Request",
                column: "IdEquipment");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_RequestIdRequest",
                table: "Equipment",
                column: "RequestIdRequest");

            migrationBuilder.CreateIndex(
                name: "IX_HomeService_IdCourier",
                table: "HomeService",
                column: "IdCourier");

            migrationBuilder.CreateIndex(
                name: "IX_HomeService_IdRequest",
                table: "HomeService",
                column: "IdRequest");

            migrationBuilder.CreateIndex(
                name: "IX_Repair_IdRequest",
                table: "Repair",
                column: "IdRequest");

            migrationBuilder.CreateIndex(
                name: "IX_Repair_IdTechnician",
                table: "Repair",
                column: "IdTechnician");

            migrationBuilder.CreateIndex(
                name: "IX_RepairPayment_IdRepair",
                table: "RepairPayment",
                column: "IdRepair");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Request_RequestIdRequest",
                table: "Equipment",
                column: "RequestIdRequest",
                principalTable: "Request",
                principalColumn: "IdRequest");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Equipment_IdEquipment",
                table: "Request",
                column: "IdEquipment",
                principalTable: "Equipment",
                principalColumn: "IdEquipment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Request_RequestIdRequest",
                table: "Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Equipment_IdEquipment",
                table: "Request");

            migrationBuilder.DropTable(
                name: "HomeService");

            migrationBuilder.DropTable(
                name: "RepairPayment");

            migrationBuilder.DropTable(
                name: "Courier");

            migrationBuilder.DropTable(
                name: "Repair");

            migrationBuilder.DropTable(
                name: "Technician");

            migrationBuilder.DropIndex(
                name: "IX_Request_IdEquipment",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_RequestIdRequest",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "IdEquipment",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "RequestIdRequest",
                table: "Equipment");

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Request",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PickUpTime",
                table: "Request",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Quote",
                table: "Request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductReview",
                columns: table => new
                {
                    IdProductReview = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRequest = table.Column<int>(type: "int", nullable: false),
                    RepairDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TechnicalRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReview", x => x.IdProductReview);
                    table.ForeignKey(
                        name: "FK_ProductReview_Request_IdRequest",
                        column: x => x.IdRequest,
                        principalTable: "Request",
                        principalColumn: "IdRequest",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_IdRequest",
                table: "Equipment",
                column: "IdRequest");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_IdRequest",
                table: "ProductReview",
                column: "IdRequest");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Request_IdRequest",
                table: "Equipment",
                column: "IdRequest",
                principalTable: "Request",
                principalColumn: "IdRequest",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
