using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WebApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountryLocations",
                columns: table => new
                {
                    GeonameId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocaleCode = table.Column<string>(nullable: true),
                    ContinentCode = table.Column<string>(nullable: true),
                    ContinentName = table.Column<string>(nullable: true),
                    CountryIsoCode = table.Column<string>(nullable: true),
                    CountryName = table.Column<string>(nullable: true),
                    IsInEuropeanUnion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryLocations", x => x.GeonameId);
                });

            migrationBuilder.CreateTable(
                name: "Ipv4bloc",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Network = table.Column<string>(nullable: true),
                    GeonameId = table.Column<long>(nullable: false),
                    RegisteredCountryGeonameId = table.Column<long>(nullable: true),
                    RepresentedCountryGeonameId = table.Column<long>(nullable: true),
                    IsAnonymousProxy = table.Column<string>(nullable: true),
                    IsSatelliteProvider = table.Column<string>(nullable: true),
                    IpMax = table.Column<long>(nullable: false),
                    IpMin = table.Column<long>(nullable: false),
                    CountryLocationGeonameId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ipv4bloc", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ipv4bloc_CountryLocations_CountryLocationGeonameId",
                        column: x => x.CountryLocationGeonameId,
                        principalTable: "CountryLocations",
                        principalColumn: "GeonameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ipv4bloc_CountryLocationGeonameId",
                table: "Ipv4bloc",
                column: "CountryLocationGeonameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ipv4bloc");

            migrationBuilder.DropTable(
                name: "CountryLocations");
        }
    }
}
