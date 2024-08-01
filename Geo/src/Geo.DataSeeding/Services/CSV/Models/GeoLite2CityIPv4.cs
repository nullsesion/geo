using CsvHelper.Configuration.Attributes;

namespace Geo.DataSeeding.Services.CSV.Models
{
	public class GeoLite2CityIPv4
	{
		[Name("network")]
		public string Network { get; set; }

		[Name("geoname_id")]
		public int? GeonameId { get; set; }

		[Name("registered_country_geoname_id")]
		public int? RegisteredCountryGeoNameId { get; set; }

		[Name("represented_country_geoname_id")]
		public int? RepresentedCountryGeoNameId { get; set; }

		[Name("is_anonymous_proxy")]
		public bool IsAnonymousProxy { get; set; }

		[Name("is_satellite_provider")]
		public bool IsSatelliteProvider { get; set; }

		[Name("is_anycast")]
		public bool? IsAnycast { get; set; } = false;

		[Name("postal_code")]
		public string PostalCode { get; set; }

		[Name("latitude")]
		public double? Latitude { get; set; } //double
		[Name("longitude")]
		public double? Longitude { get; set; } //double

		[Name("accuracy_radius")]
		public int? AccuracyRadius { get; set; } = 0; //AccuracyRadius
	}
}
