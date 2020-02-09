using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace loadMaxmind.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountryLocations",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    GeonameId = table.Column<string>(nullable: true),
                    LocaleCode = table.Column<string>(nullable: true),
                    ContinentCode = table.Column<string>(nullable: true),
                    ContinentName = table.Column<string>(nullable: true),
                    CountryIsoCode = table.Column<string>(nullable: true),
                    CountryName = table.Column<string>(nullable: true),
                    IsInEuropeanUnion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryLocations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ipv4bloc",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Network = table.Column<string>(nullable: true),
                    GeonameId = table.Column<string>(nullable: true),
                    RegisteredCountryGeonameId = table.Column<string>(nullable: true),
                    RepresentedCountryGeonameId = table.Column<string>(nullable: true),
                    IsAnonymousProxy = table.Column<string>(nullable: true),
                    IsSatelliteProvider = table.Column<string>(nullable: true),
                    IpMax = table.Column<long>(nullable: false),
                    IpMin = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ipv4bloc", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryLocations");

            migrationBuilder.DropTable(
                name: "Ipv4bloc");
        }
    }
}
