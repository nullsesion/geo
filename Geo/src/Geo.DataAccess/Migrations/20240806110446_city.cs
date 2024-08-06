using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace Geo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class city : Migration
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

            migrationBuilder.CreateIndex(
                name: "IX_CityIPv4s_IpMax",
                table: "CityIPv4s",
                column: "IpMax");

            migrationBuilder.CreateIndex(
                name: "IX_CityIPv4s_IpMin",
                table: "CityIPv4s",
                column: "IpMin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityIPv4s");
        }
    }
}
