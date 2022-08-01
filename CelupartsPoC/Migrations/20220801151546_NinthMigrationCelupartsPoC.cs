using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelupartsPoC.Migrations
{
    public partial class NinthMigrationCelupartsPoC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductInReturn",
                table: "RequestStates",
                newName: "ProductReturned");

            migrationBuilder.AddColumn<string>(
                name: "TechnicalRemarks",
                table: "ProductReview",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TechnicalRemarks",
                table: "ProductReview");

            migrationBuilder.RenameColumn(
                name: "ProductReturned",
                table: "RequestStates",
                newName: "ProductInReturn");
        }
    }
}
