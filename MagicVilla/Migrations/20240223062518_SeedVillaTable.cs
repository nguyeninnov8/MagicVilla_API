using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedAt", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Villa 1 Amenity", new DateTime(2024, 2, 23, 13, 25, 17, 932, DateTimeKind.Local).AddTicks(9837), "Villa 1 Details", "https://via.placeholder.com/150", "Villa 1", 4, 100.0, 1000, null },
                    { 2, "Villa 2 Amenity", new DateTime(2024, 2, 23, 13, 25, 17, 932, DateTimeKind.Local).AddTicks(9853), "Villa 2 Details", "https://via.placeholder.com/150", "Villa 2", 6, 200.0, 2000, null },
                    { 3, "Villa 3 Amenity", new DateTime(2024, 2, 23, 13, 25, 17, 932, DateTimeKind.Local).AddTicks(9855), "Villa 3 Details", "https://via.placeholder.com/150", "Villa 3", 8, 300.0, 3000, null },
                    { 4, "Villa 4 Amenity", new DateTime(2024, 2, 23, 13, 25, 17, 932, DateTimeKind.Local).AddTicks(9857), "Villa 4 Details", "https://via.placeholder.com/150", "Villa 4", 10, 400.0, 4000, null },
                    { 5, "Villa 5 Amenity", new DateTime(2024, 2, 23, 13, 25, 17, 932, DateTimeKind.Local).AddTicks(9859), "Villa 5 Details", "https://via.placeholder.com/150", "Villa 5", 12, 500.0, 5000, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
