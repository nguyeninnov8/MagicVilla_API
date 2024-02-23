using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 2, 23, 13, 35, 45, 907, DateTimeKind.Local).AddTicks(8816), new DateTime(2024, 2, 23, 13, 35, 45, 907, DateTimeKind.Local).AddTicks(8834) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 2, 23, 13, 35, 45, 907, DateTimeKind.Local).AddTicks(8837), new DateTime(2024, 2, 23, 13, 35, 45, 907, DateTimeKind.Local).AddTicks(8837) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 2, 23, 13, 35, 45, 907, DateTimeKind.Local).AddTicks(8839), new DateTime(2024, 2, 23, 13, 35, 45, 907, DateTimeKind.Local).AddTicks(8840) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 2, 23, 13, 35, 45, 907, DateTimeKind.Local).AddTicks(8841), new DateTime(2024, 2, 23, 13, 35, 45, 907, DateTimeKind.Local).AddTicks(8842) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 2, 23, 13, 35, 45, 907, DateTimeKind.Local).AddTicks(8843), new DateTime(2024, 2, 23, 13, 35, 45, 907, DateTimeKind.Local).AddTicks(8844) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 2, 23, 13, 33, 55, 309, DateTimeKind.Local).AddTicks(441), null });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 2, 23, 13, 33, 55, 309, DateTimeKind.Local).AddTicks(457), null });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 2, 23, 13, 33, 55, 309, DateTimeKind.Local).AddTicks(459), null });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 2, 23, 13, 33, 55, 309, DateTimeKind.Local).AddTicks(461), null });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 2, 23, 13, 33, 55, 309, DateTimeKind.Local).AddTicks(462), null });
        }
    }
}
