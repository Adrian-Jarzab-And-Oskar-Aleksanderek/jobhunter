using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5aa13d1-6a24-4363-80a9-11ab8bda24cb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8be0b1b-e890-4add-ac41-eca0f4b9affe");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "33383015-db2c-4a32-b9e7-9b0f3c48604c", null, "Admin", "ADMIN" },
                    { "7c38015c-2279-4f21-a567-1e97782125f9", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33383015-db2c-4a32-b9e7-9b0f3c48604c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c38015c-2279-4f21-a567-1e97782125f9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d5aa13d1-6a24-4363-80a9-11ab8bda24cb", null, "Admin", "ADMIN" },
                    { "f8be0b1b-e890-4add-ac41-eca0f4b9affe", null, "User", "USER" }
                });
        }
    }
}
