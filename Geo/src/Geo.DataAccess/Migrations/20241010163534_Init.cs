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
                name: "city_locations",
                columns: table => new
                {
                    geoname_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    continent_code = table.Column<string>(type: "text", nullable: false),
                    continent_name = table.Column<string>(type: "text", nullable: false),
                    country_iso_code = table.Column<string>(type: "text", nullable: false),
                    country_name = table.Column<string>(type: "text", nullable: false),
                    subdivision = table.Column<string>(type: "text", nullable: false),
                    city_name = table.Column<string>(type: "text", nullable: false),
                    metro_code = table.Column<int>(type: "integer", nullable: true),
                    time_zone = table.Column<string>(type: "text", nullable: false),
                    is_in_european_union = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_city_locations", x => x.geoname_id);
                });

            migrationBuilder.CreateTable(
                name: "country_locations",
                columns: table => new
                {
                    geoname_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    continent_code = table.Column<string>(type: "text", nullable: false),
                    continent_name = table.Column<string>(type: "jsonb", nullable: false),
                    country_iso_code = table.Column<string>(type: "text", nullable: false),
                    country_name = table.Column<string>(type: "jsonb", nullable: false),
                    is_in_european_union = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_country_locations", x => x.geoname_id);
                });

            migrationBuilder.CreateTable(
                name: "city_i_pv4s",
                columns: table => new
                {
                    network = table.Column<string>(type: "text", nullable: false),
                    ip_min = table.Column<int>(type: "integer", nullable: false),
                    ip_max = table.Column<int>(type: "integer", nullable: false),
                    geoname_id = table.Column<int>(type: "integer", nullable: true),
                    registered_country_geo_name_id = table.Column<int>(type: "integer", nullable: true),
                    represented_country_geo_name_id = table.Column<int>(type: "integer", nullable: true),
                    is_anonymous_proxy = table.Column<bool>(type: "boolean", nullable: false),
                    is_satellite_provider = table.Column<bool>(type: "boolean", nullable: false),
                    is_anycast = table.Column<bool>(type: "boolean", nullable: true),
                    location = table.Column<NpgsqlPoint>(type: "point", nullable: true),
                    accuracy_radius = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_city_i_pv4s", x => x.network);
                    table.ForeignKey(
                        name: "fk_city_i_pv4s_city_locations_geoname_id",
                        column: x => x.geoname_id,
                        principalTable: "city_locations",
                        principalColumn: "geoname_id");
                    table.ForeignKey(
                        name: "fk_city_i_pv4s_city_locations_registered_country_geo_name_id",
                        column: x => x.registered_country_geo_name_id,
                        principalTable: "city_locations",
                        principalColumn: "geoname_id");
                    table.ForeignKey(
                        name: "fk_city_i_pv4s_city_locations_represented_country_geo_name_id",
                        column: x => x.represented_country_geo_name_id,
                        principalTable: "city_locations",
                        principalColumn: "geoname_id");
                });

            migrationBuilder.CreateTable(
                name: "country_i_pv4s",
                columns: table => new
                {
                    network = table.Column<string>(type: "text", nullable: false),
                    ip_min = table.Column<int>(type: "integer", nullable: false),
                    ip_max = table.Column<int>(type: "integer", nullable: false),
                    geoname_id = table.Column<int>(type: "integer", nullable: true),
                    registered_country_geo_name_id = table.Column<int>(type: "integer", nullable: true),
                    represented_country_geo_name_id = table.Column<int>(type: "integer", nullable: true),
                    is_anonymous_proxy = table.Column<bool>(type: "boolean", nullable: false),
                    is_satellite_provider = table.Column<bool>(type: "boolean", nullable: false),
                    is_anycast = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_country_i_pv4s", x => x.network);
                    table.ForeignKey(
                        name: "fk_country_i_pv4s_country_locations_geoname_id",
                        column: x => x.geoname_id,
                        principalTable: "country_locations",
                        principalColumn: "geoname_id");
                    table.ForeignKey(
                        name: "fk_country_i_pv4s_country_locations_registered_country_geo_nam",
                        column: x => x.registered_country_geo_name_id,
                        principalTable: "country_locations",
                        principalColumn: "geoname_id");
                    table.ForeignKey(
                        name: "fk_country_i_pv4s_country_locations_represented_country_geo_na",
                        column: x => x.represented_country_geo_name_id,
                        principalTable: "country_locations",
                        principalColumn: "geoname_id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_city_i_pv4s_geoname_id",
                table: "city_i_pv4s",
                column: "geoname_id");

            migrationBuilder.CreateIndex(
                name: "ix_city_i_pv4s_ip_max",
                table: "city_i_pv4s",
                column: "ip_max");

            migrationBuilder.CreateIndex(
                name: "ix_city_i_pv4s_ip_min",
                table: "city_i_pv4s",
                column: "ip_min");

            migrationBuilder.CreateIndex(
                name: "ix_city_i_pv4s_registered_country_geo_name_id",
                table: "city_i_pv4s",
                column: "registered_country_geo_name_id");

            migrationBuilder.CreateIndex(
                name: "ix_city_i_pv4s_represented_country_geo_name_id",
                table: "city_i_pv4s",
                column: "represented_country_geo_name_id");

            migrationBuilder.CreateIndex(
                name: "ix_country_i_pv4s_geoname_id",
                table: "country_i_pv4s",
                column: "geoname_id");

            migrationBuilder.CreateIndex(
                name: "ix_country_i_pv4s_ip_max",
                table: "country_i_pv4s",
                column: "ip_max");

            migrationBuilder.CreateIndex(
                name: "ix_country_i_pv4s_ip_min",
                table: "country_i_pv4s",
                column: "ip_min");

            migrationBuilder.CreateIndex(
                name: "ix_country_i_pv4s_registered_country_geo_name_id",
                table: "country_i_pv4s",
                column: "registered_country_geo_name_id");

            migrationBuilder.CreateIndex(
                name: "ix_country_i_pv4s_represented_country_geo_name_id",
                table: "country_i_pv4s",
                column: "represented_country_geo_name_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "city_i_pv4s");

            migrationBuilder.DropTable(
                name: "country_i_pv4s");

            migrationBuilder.DropTable(
                name: "city_locations");

            migrationBuilder.DropTable(
                name: "country_locations");
        }
    }
}
