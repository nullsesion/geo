namespace Geo.Api.Models
{
	public class CityInfo
	{
		public string ContinentCode { get; set; }
		public string ContinentName { get; set; }
		public string CountryIsoCode { get; set; }
		public string CountryName { get; set; }
		public string? Subdivision1IsoCode { get; set; }
		public string? Subdivision1Name { get; set; }
		public string? Subdivision2IsoCode { get; set; }
		public string? Subdivision2Name { get; set; }
		public string? CityName { get; set; }
		public string? MetroCode { get; set; }
		public string? TimeZone { get; set; }
		public bool IsInEuropeanUnion { get; set; }
	}
}
