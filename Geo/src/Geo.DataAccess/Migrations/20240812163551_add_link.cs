using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Geo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_link : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CountryIPv4s_GeonameId",
                table: "CountryIPv4s",
                column: "GeonameId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryIPv4s_RegisteredCountryGeoNameId",
                table: "CountryIPv4s",
                column: "RegisteredCountryGeoNameId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryIPv4s_RepresentedCountryGeoNameId",
                table: "CountryIPv4s",
                column: "RepresentedCountryGeoNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_CountryIPv4s_CountryLocations_GeonameId",
                table: "CountryIPv4s",
                column: "GeonameId",
                principalTable: "CountryLocations",
                principalColumn: "GeonameId");

            migrationBuilder.AddForeignKey(
                name: "FK_CountryIPv4s_CountryLocations_RegisteredCountryGeoNameId",
                table: "CountryIPv4s",
                column: "RegisteredCountryGeoNameId",
                principalTable: "CountryLocations",
                principalColumn: "GeonameId");

            migrationBuilder.AddForeignKey(
                name: "FK_CountryIPv4s_CountryLocations_RepresentedCountryGeoNameId",
                table: "CountryIPv4s",
                column: "RepresentedCountryGeoNameId",
                principalTable: "CountryLocations",
                principalColumn: "GeonameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountryIPv4s_CountryLocations_GeonameId",
                table: "CountryIPv4s");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryIPv4s_CountryLocations_RegisteredCountryGeoNameId",
                table: "CountryIPv4s");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryIPv4s_CountryLocations_RepresentedCountryGeoNameId",
                table: "CountryIPv4s");

            migrationBuilder.DropIndex(
                name: "IX_CountryIPv4s_GeonameId",
                table: "CountryIPv4s");

            migrationBuilder.DropIndex(
                name: "IX_CountryIPv4s_RegisteredCountryGeoNameId",
                table: "CountryIPv4s");

            migrationBuilder.DropIndex(
                name: "IX_CountryIPv4s_RepresentedCountryGeoNameId",
                table: "CountryIPv4s");
        }
    }
}
