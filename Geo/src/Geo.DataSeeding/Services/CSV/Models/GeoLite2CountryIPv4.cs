using CsvHelper.Configuration.Attributes;
using Geo.DomainShared.Contracts;

namespace Geo.DataSeeding.Services.CSV.Models
{
	public class GeoLite2CountryIPv4: ICsvModel, ICountryIPv4Range
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
		public bool? IsAnycast { get; set; }


		public string GetPartFileName()
		{
			return "GeoLite2-Country-Blocks-IPv4";
		}
	}
}
