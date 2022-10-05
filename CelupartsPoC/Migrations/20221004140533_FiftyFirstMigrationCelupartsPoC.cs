using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelupartsPoC.Migrations
{
    public partial class FiftyFirstMigrationCelupartsPoC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_TypeOfEquipment_IdTypeOfEquipment",
                table: "Equipment");

            migrationBuilder.AlterColumn<int>(
                name: "IdTypeOfEquipment",
                table: "Equipment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "EquipmentBrand",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_TypeOfEquipment_IdTypeOfEquipment",
                table: "Equipment",
                column: "IdTypeOfEquipment",
                principalTable: "TypeOfEquipment",
                principalColumn: "IdTypeOfEquipment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_TypeOfEquipment_IdTypeOfEquipment",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "EquipmentBrand",
                table: "Equipment");

            migrationBuilder.AlterColumn<int>(
                name: "IdTypeOfEquipment",
                table: "Equipment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_TypeOfEquipment_IdTypeOfEquipment",
                table: "Equipment",
                column: "IdTypeOfEquipment",
                principalTable: "TypeOfEquipment",
                principalColumn: "IdTypeOfEquipment",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
