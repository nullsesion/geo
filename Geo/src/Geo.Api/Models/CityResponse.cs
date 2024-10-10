using Geo.Domain;

namespace Geo.Api.Models
{
	public class CityResponse
	{
		public string Network { get; set; }
		public bool IsAnonymousProxy { get; set; }
		public bool IsSatelliteProvider { get; set; }
		public bool IsAnycast { get; set; }
		public Coordinate Location { get; set; }
		public CityInfo Geoname { get; set; }
		public CityInfo RegisteredCountryGeoName { get; set; }
		public CityInfo RepresentedCountryGeoName { get; set; }
	}
}
