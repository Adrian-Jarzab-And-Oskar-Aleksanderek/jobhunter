using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddManyToManyRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_NiceToHaveSkills_NiceToHaveSkillsId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_RequierdSkills_RequierdSkillsId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_NiceToHaveSkillsId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_RequierdSkillsId",
                table: "Skills");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d8bbedd-e3a0-4f6e-a9c5-dcfd088120f3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ad1d3e5-8383-49d6-afe0-1c9854fda9ba");

            migrationBuilder.DropColumn(
                name: "NiceToHaveSkillsId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "RequierdSkillsId",
                table: "Skills");

            migrationBuilder.CreateTable(
                name: "NiceToHaveSkills_Skills",
                columns: table => new
                {
                    NiceToHaveSkillsId = table.Column<int>(type: "integer", nullable: false),
                    SkillsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NiceToHaveSkills_Skills", x => new { x.NiceToHaveSkillsId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_NiceToHaveSkills_Skills_NiceToHaveSkills_NiceToHaveSkillsId",
                        column: x => x.NiceToHaveSkillsId,
                        principalTable: "NiceToHaveSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NiceToHaveSkills_Skills_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequierdSkills_Skills",
                columns: table => new
                {
                    RequierdSkillsId = table.Column<int>(type: "integer", nullable: false),
                    SkillsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequierdSkills_Skills", x => new { x.RequierdSkillsId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_RequierdSkills_Skills_RequierdSkills_RequierdSkillsId",
                        column: x => x.RequierdSkillsId,
                        principalTable: "RequierdSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequierdSkills_Skills_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ca86f4a8-f953-448e-a15e-414803a138e9", null, "User", "USER" },
                    { "d08852b5-476b-4474-96ca-b54941410bb8", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_NiceToHaveSkills_Skills_SkillsId",
                table: "NiceToHaveSkills_Skills",
                column: "SkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_RequierdSkills_Skills_SkillsId",
                table: "RequierdSkills_Skills",
                column: "SkillsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NiceToHaveSkills_Skills");

            migrationBuilder.DropTable(
                name: "RequierdSkills_Skills");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca86f4a8-f953-448e-a15e-414803a138e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d08852b5-476b-4474-96ca-b54941410bb8");

            migrationBuilder.AddColumn<int>(
                name: "NiceToHaveSkillsId",
                table: "Skills",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequierdSkillsId",
                table: "Skills",
                type: "integer",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d8bbedd-e3a0-4f6e-a9c5-dcfd088120f3", null, "Admin", "ADMIN" },
                    { "6ad1d3e5-8383-49d6-afe0-1c9854fda9ba", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_NiceToHaveSkillsId",
                table: "Skills",
                column: "NiceToHaveSkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_RequierdSkillsId",
                table: "Skills",
                column: "RequierdSkillsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_NiceToHaveSkills_NiceToHaveSkillsId",
                table: "Skills",
                column: "NiceToHaveSkillsId",
                principalTable: "NiceToHaveSkills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_RequierdSkills_RequierdSkillsId",
                table: "Skills",
                column: "RequierdSkillsId",
                principalTable: "RequierdSkills",
                principalColumn: "Id");
        }
    }
}
