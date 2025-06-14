using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class RecreateDatabaseV5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00f90c15-0516-4e40-a2e6-baad27aa1613");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9bc1c7d4-7265-48fc-b92f-8681da9942c0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6921442d-26c4-44e9-9117-dbf430569a9e", null, "User", "USER" },
                    { "fe55a6f5-d329-4f13-98f0-ab13ea2a4585", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6921442d-26c4-44e9-9117-dbf430569a9e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe55a6f5-d329-4f13-98f0-ab13ea2a4585");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00f90c15-0516-4e40-a2e6-baad27aa1613", null, "Admin", "ADMIN" },
                    { "9bc1c7d4-7265-48fc-b92f-8681da9942c0", null, "User", "USER" }
                });
        }
    }
}
