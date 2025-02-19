using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddJobOfferSkillsRelation3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d75c6ede-6d71-4e78-afd4-c7dcac9d9ce4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9c9aba9-931a-4aaa-aba3-f7fa82e4aead");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f84cfa9-7cf2-45d7-a6c5-4f721c61b4e9", null, "Admin", "ADMIN" },
                    { "9c46bb07-153c-4841-b8c6-e7e782be0a94", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f84cfa9-7cf2-45d7-a6c5-4f721c61b4e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c46bb07-153c-4841-b8c6-e7e782be0a94");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d75c6ede-6d71-4e78-afd4-c7dcac9d9ce4", null, "User", "USER" },
                    { "d9c9aba9-931a-4aaa-aba3-f7fa82e4aead", null, "Admin", "ADMIN" }
                });
        }
    }
}
