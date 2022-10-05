using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelupartsPoC.Migrations
{
    public partial class FiftiethMigrationCelupartsPoC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EquipmentBrand",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "TypeOfEquipment",
                table: "Equipment");

            migrationBuilder.AddColumn<int>(
                name: "IdTypeOfEquipment",
                table: "Equipment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdTypeOfEquipment",
                table: "Brand",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_IdTypeOfEquipment",
                table: "Equipment",
                column: "IdTypeOfEquipment");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_IdTypeOfEquipment",
                table: "Brand",
                column: "IdTypeOfEquipment");

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_TypeOfEquipment_IdTypeOfEquipment",
                table: "Brand",
                column: "IdTypeOfEquipment",
                principalTable: "TypeOfEquipment",
                principalColumn: "IdTypeOfEquipment",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_TypeOfEquipment_IdTypeOfEquipment",
                table: "Equipment",
                column: "IdTypeOfEquipment",
                principalTable: "TypeOfEquipment",
                principalColumn: "IdTypeOfEquipment",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_TypeOfEquipment_IdTypeOfEquipment",
                table: "Brand");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_TypeOfEquipment_IdTypeOfEquipment",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_IdTypeOfEquipment",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Brand_IdTypeOfEquipment",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "IdTypeOfEquipment",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "IdTypeOfEquipment",
                table: "Brand");

            migrationBuilder.AddColumn<string>(
                name: "EquipmentBrand",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TypeOfEquipment",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
