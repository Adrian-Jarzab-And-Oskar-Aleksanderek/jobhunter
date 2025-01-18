using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    RequiredSkills = table.Column<List<string>>(type: "text[]", nullable: false),
                    NiceToHaveSkills = table.Column<List<string>>(type: "text[]", nullable: false),
                    WorkplaceType = table.Column<string>(type: "text", nullable: false),
                    WorkingTime = table.Column<string>(type: "text", nullable: false),
                    ExperienceLevel = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    RemoteInterview = table.Column<bool>(type: "boolean", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    CompanyLogoThumbUrl = table.Column<string>(type: "text", nullable: false),
                    PublishedAt = table.Column<string>(type: "text", nullable: false),
                    OpenToHireUkrainians = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOffers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    To = table.Column<double>(type: "double precision", nullable: true),
                    From = table.Column<double>(type: "double precision", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Gross = table.Column<bool>(type: "boolean", nullable: false),
                    To_Chf = table.Column<double>(type: "double precision", nullable: true),
                    To_Eur = table.Column<double>(type: "double precision", nullable: true),
                    To_Gbp = table.Column<double>(type: "double precision", nullable: true),
                    To_Pln = table.Column<double>(type: "double precision", nullable: true),
                    To_usd = table.Column<double>(type: "double precision", nullable: true),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    From_Chf = table.Column<double>(type: "double precision", nullable: true),
                    From_Eur = table.Column<double>(type: "double precision", nullable: true),
                    From_Gbp = table.Column<double>(type: "double precision", nullable: true),
                    From_Pln = table.Column<double>(type: "double precision", nullable: true),
                    From_Usd = table.Column<double>(type: "double precision", nullable: true),
                    JobOfferId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmploymentTypes_JobOffers_JobOfferId",
                        column: x => x.JobOfferId,
                        principalTable: "JobOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultiLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    City = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    JobOfferId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultiLocations_JobOffers_JobOfferId",
                        column: x => x.JobOfferId,
                        principalTable: "JobOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentTypes_JobOfferId",
                table: "EmploymentTypes",
                column: "JobOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiLocations_JobOfferId",
                table: "MultiLocations",
                column: "JobOfferId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmploymentTypes");

            migrationBuilder.DropTable(
                name: "MultiLocations");

            migrationBuilder.DropTable(
                name: "JobOffers");
        }
    }
}
