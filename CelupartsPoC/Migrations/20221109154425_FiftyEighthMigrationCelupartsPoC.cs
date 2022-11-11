using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelupartsPoC.Migrations
{
    public partial class FiftyEighthMigrationCelupartsPoC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartsToRepair",
                columns: table => new
                {
                    IdPartsToRepair = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRepair = table.Column<int>(type: "int", nullable: false),
                    Part = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToReplace = table.Column<bool>(type: "bit", nullable: false),
                    ToRepair = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartsToRepair", x => x.IdPartsToRepair);
                    table.ForeignKey(
                        name: "FK_PartsToRepair_Repair_IdRepair",
                        column: x => x.IdRepair,
                        principalTable: "Repair",
                        principalColumn: "IdRepair",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartsToRepair_IdRepair",
                table: "PartsToRepair",
                column: "IdRepair");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartsToRepair");
        }
    }
}
