﻿using CsvHelper.Configuration.Attributes;

namespace Geo.DataSeeding.Services.CSV.Models
{
	public class GeoLite2CityLocations
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

		[Name("subdivision_1_iso_code")]
		public string Subdivision1IsoCode { get; set; }
		[Name("subdivision_1_name")]
		public string Subdivision1Name { get; set; }

		[Name("subdivision_2_iso_code")]
		public string Subdivision2IsoCode { get; set; }

		[Name("subdivision_2_name")]
		public string Subdivision2Name { get; set; }

		[Name("city_name")]
		public string CityName { get; set; }

		[Name("metro_code")]
		public int? MetroCode { get; set; }

		[Name("subdivision_2_name")]
		public string TimeZone { get; set; }

		[Name("is_in_european_union")]
		public bool IsInEuropeanUnion { get; set; } = false;
	}
}
