using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddJobOfferSkillsRelation6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b255b6c-b033-4819-a3e9-5eccae4eec08");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98a36f34-0ed3-4e9e-b963-9dd763eb5e94");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d5aa13d1-6a24-4363-80a9-11ab8bda24cb", null, "Admin", "ADMIN" },
                    { "f8be0b1b-e890-4add-ac41-eca0f4b9affe", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "7b255b6c-b033-4819-a3e9-5eccae4eec08", null, "User", "USER" },
                    { "98a36f34-0ed3-4e9e-b963-9dd763eb5e94", null, "Admin", "ADMIN" }
                });
        }
    }
}
