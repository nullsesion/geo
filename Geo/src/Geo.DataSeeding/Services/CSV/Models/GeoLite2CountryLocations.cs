using CsvHelper.Configuration.Attributes;
using Geo.DomainShared.Contracts;

namespace Geo.DataSeeding.Services.CSV.Models
{
	public class GeoLite2CountryLocations: ICountryLocation, ILocaleCode
	{
		[Name("geoname_id")]
		public int GeonameId { get; set; }

		[Name("locale_code")]
		public string LocaleCode { get; set; }

		[Name("continent_code")]
		public string ContinentCode { get; set; }

		[Name("continent_name")]
		public string ContinentName { get; set; }

		[Name("country_iso_code")]
		public string CountryIsoCode { get; set; }

		[Name("country_name")]
		public string CountryName { get; set; }

		[Name("is_in_european_union")]
		public bool IsInEuropeanUnion { get; set; } = false;
	}
}
