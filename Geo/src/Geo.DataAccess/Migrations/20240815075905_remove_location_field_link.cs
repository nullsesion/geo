using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Geo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class remove_location_field_link : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocaleCode",
                table: "CityLocations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocaleCode",
                table: "CityLocations",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
