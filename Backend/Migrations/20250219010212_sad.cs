using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class sad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f334b8d-1519-4055-9db9-5f09106eca21");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f86a1bd-fe28-4db2-ac3b-48687a293a32");

            migrationBuilder.AlterColumn<List<string>>(
                name: "RequiredSkills",
                table: "JobOffers",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "text[]");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2394d278-70d5-4ec5-85e1-0479bfbebc10", null, "User", "USER" },
                    { "70853228-a4dc-417c-977f-e4b43dcbe6fb", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2394d278-70d5-4ec5-85e1-0479bfbebc10");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70853228-a4dc-417c-977f-e4b43dcbe6fb");

            migrationBuilder.AlterColumn<List<string>>(
                name: "RequiredSkills",
                table: "JobOffers",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f334b8d-1519-4055-9db9-5f09106eca21", null, "User", "USER" },
                    { "8f86a1bd-fe28-4db2-ac3b-48687a293a32", null, "Admin", "ADMIN" }
                });
        }
    }
}
