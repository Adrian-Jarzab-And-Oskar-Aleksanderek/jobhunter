using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class RecreateDatabaseV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "861a5bd4-1417-48cf-9b6f-7dd3f7ff44c2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b552cdf5-7263-4742-8442-5826d3be9f8c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00f90c15-0516-4e40-a2e6-baad27aa1613", null, "Admin", "ADMIN" },
                    { "9bc1c7d4-7265-48fc-b92f-8681da9942c0", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "861a5bd4-1417-48cf-9b6f-7dd3f7ff44c2", null, "User", "USER" },
                    { "b552cdf5-7263-4742-8442-5826d3be9f8c", null, "Admin", "ADMIN" }
                });
        }
    }
}
