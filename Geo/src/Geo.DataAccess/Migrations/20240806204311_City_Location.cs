using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Geo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class City_Location : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityLocations");
        }
    }
}
