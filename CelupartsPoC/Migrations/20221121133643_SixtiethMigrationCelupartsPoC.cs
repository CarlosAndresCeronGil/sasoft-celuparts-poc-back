using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelupartsPoC.Migrations
{
    public partial class SixtiethMigrationCelupartsPoC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PriceReviewedByAdmin",
                table: "RetomaPayment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PriceReviewedByAdmin",
                table: "RepairPayment",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceReviewedByAdmin",
                table: "RetomaPayment");

            migrationBuilder.DropColumn(
                name: "PriceReviewedByAdmin",
                table: "RepairPayment");
        }
    }
}
