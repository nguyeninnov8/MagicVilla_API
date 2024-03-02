using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForeignKeyForVillaNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "villaId",
                table: "VillasNumber",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VillasNumber_villaId",
                table: "VillasNumber",
                column: "villaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VillasNumber_Villas_villaId",
                table: "VillasNumber",
                column: "villaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillasNumber_Villas_villaId",
                table: "VillasNumber");

            migrationBuilder.DropIndex(
                name: "IX_VillasNumber_villaId",
                table: "VillasNumber");

            migrationBuilder.DropColumn(
                name: "villaId",
                table: "VillasNumber");
        }
    }
}
