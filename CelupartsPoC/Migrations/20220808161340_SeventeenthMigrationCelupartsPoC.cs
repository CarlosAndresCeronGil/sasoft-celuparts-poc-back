using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelupartsPoC.Migrations
{
    public partial class SeventeenthMigrationCelupartsPoC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Request_RequestIdRequest",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_RequestIdRequest",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "RequestIdRequest",
                table: "Equipment");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RepairDate",
                table: "Repair",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RepairDate",
                table: "Repair",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestIdRequest",
                table: "Equipment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_RequestIdRequest",
                table: "Equipment",
                column: "RequestIdRequest");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Request_RequestIdRequest",
                table: "Equipment",
                column: "RequestIdRequest",
                principalTable: "Request",
                principalColumn: "IdRequest");
        }
    }
}
