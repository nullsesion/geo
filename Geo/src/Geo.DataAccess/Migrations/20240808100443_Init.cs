using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

#nullable disable

namespace Geo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CityIPv4s",
                columns: table => new
                {
                    Network = table.Column<string>(type: "text", nullable: false),
                    IpMin = table.Column<int>(type: "integer", nullable: false),
                    IpMax = table.Column<int>(type: "integer", nullable: false),
                    GeonameId = table.Column<int>(type: "integer", nullable: true),
                    RegisteredCountryGeoNameId = table.Column<int>(type: "integer", nullable: true),
                    RepresentedCountryGeoNameId = table.Column<int>(type: "integer", nullable: true),
                    IsAnonymousProxy = table.Column<bool>(type: "boolean", nullable: false),
                    IsSatelliteProvider = table.Column<bool>(type: "boolean", nullable: false),
                    IsAnycast = table.Column<bool>(type: "boolean", nullable: true),
                    Location = table.Column<NpgsqlPoint>(type: "point", nullable: true),
                    AccuracyRadius = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityIPv4s", x => x.Network);
                });

            migrationBuilder.CreateTable(
                name: "CityLocations",
                columns: table => new
                {
                    GeonameId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocaleCode = table.Column<string>(type: "text", nullable: false),
                    ContinentCode = table.Column<string>(type: "text", nullable: false),
                    ContinentName = table.Column<string>(type: "text", nullable: false),
                    CountryIsoCode = table.Column<string>(type: "text", nullable: false),
                    CountryName = table.Column<string>(type: "text", nullable: false),
                    Subdivision = table.Column<string>(type: "text", nullable: false),
                    CityName = table.Column<string>(type: "text", nullable: false),
                    MetroCode = table.Column<int>(type: "integer", nullable: true),
                    TimeZone = table.Column<string>(type: "text", nullable: false),
                    IsInEuropeanUnion = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityLocations", x => x.GeonameId);
                });

            migrationBuilder.CreateTable(
                name: "CountryIPv4s",
                columns: table => new
                {
                    Network = table.Column<string>(type: "text", nullable: false),
                    IpMin = table.Column<int>(type: "integer", nullable: false),
                    IpMax = table.Column<int>(type: "integer", nullable: false),
                    GeonameId = table.Column<int>(type: "integer", nullable: true),
                    RegisteredCountryGeoNameId = table.Column<int>(type: "integer", nullable: true),
                    RepresentedCountryGeoNameId = table.Column<int>(type: "integer", nullable: true),
                    IsAnonymousProxy = table.Column<bool>(type: "boolean", nullable: false),
                    IsSatelliteProvider = table.Column<bool>(type: "boolean", nullable: false),
                    IsAnycast = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryIPv4s", x => x.Network);
                });

            migrationBuilder.CreateTable(
                name: "CountryLocations",
                columns: table => new
                {
                    GeonameId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContinentCode = table.Column<string>(type: "text", nullable: false),
                    ContinentName = table.Column<string>(type: "jsonb", nullable: false),
                    CountryIsoCode = table.Column<string>(type: "text", nullable: false),
                    CountryName = table.Column<string>(type: "jsonb", nullable: false),
                    IsInEuropeanUnion = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryLocations", x => x.GeonameId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityIPv4s_IpMax",
                table: "CityIPv4s",
                column: "IpMax");

            migrationBuilder.CreateIndex(
                name: "IX_CityIPv4s_IpMin",
                table: "CityIPv4s",
                column: "IpMin");

            migrationBuilder.CreateIndex(
                name: "IX_CountryIPv4s_IpMax",
                table: "CountryIPv4s",
                column: "IpMax");

            migrationBuilder.CreateIndex(
                name: "IX_CountryIPv4s_IpMin",
                table: "CountryIPv4s",
                column: "IpMin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityIPv4s");

            migrationBuilder.DropTable(
                name: "CityLocations");

            migrationBuilder.DropTable(
                name: "CountryIPv4s");

            migrationBuilder.DropTable(
                name: "CountryLocations");
        }
    }
}
