using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class RecreateDatabaseV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f90de89-b0e1-447f-8581-07efed3d23b9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc40586b-8849-4c2b-b836-328dc4d77621");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "861a5bd4-1417-48cf-9b6f-7dd3f7ff44c2", null, "User", "USER" },
                    { "b552cdf5-7263-4742-8442-5826d3be9f8c", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "6f90de89-b0e1-447f-8581-07efed3d23b9", null, "User", "USER" },
                    { "fc40586b-8849-4c2b-b836-328dc4d77621", null, "Admin", "ADMIN" }
                });
        }
    }
}
