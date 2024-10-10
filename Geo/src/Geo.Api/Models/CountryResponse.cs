namespace Geo.Api.Models
{
	public class CountryResponse
	{
		public string Network { get; set; }
		public bool IsAnonymousProxy { get; set; }
		public bool IsSatelliteProvider { get; set; }
		public bool IsAnycast { get; set; }
		public CountryInfo Geoname { get; set; }
		public CountryInfo RegisteredCountryGeoName { get; set; }
		public CountryInfo RepresentedCountryGeoName { get; set; }
	}
}
