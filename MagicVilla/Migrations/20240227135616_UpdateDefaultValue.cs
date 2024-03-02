using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDefaultValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_villasNumber",
                table: "villasNumber");

            migrationBuilder.RenameTable(
                name: "villasNumber",
                newName: "VillasNumber");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "VillasNumber",
                newName: "UpdatedAt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VillasNumber",
                table: "VillasNumber",
                column: "VillaNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VillasNumber",
                table: "VillasNumber");

            migrationBuilder.RenameTable(
                name: "VillasNumber",
                newName: "villasNumber");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "villasNumber",
                newName: "UpdatedDAt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_villasNumber",
                table: "villasNumber",
                column: "VillaNo");
        }
    }
}
