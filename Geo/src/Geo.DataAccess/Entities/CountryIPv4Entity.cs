using Geo.DomainShared.Contracts;

namespace Geo.DataAccess.Entities
{
	public class CountryIPv4Entity: ICountryIPv4Range
	{
		public string Network { get; set; }
		public int IpMin { get; set; }
		public int IpMax { get; set; }
		public int? GeonameId { get; set; }
		public int? RegisteredCountryGeoNameId { get; set; }
		public int? RepresentedCountryGeoNameId { get; set; }
		public bool IsAnonymousProxy { get; set; }
		public bool IsSatelliteProvider { get; set; }
		public bool? IsAnycast { get; set; }

		public CountryLocationEntity Geoname { get; set; }
		public CountryLocationEntity? RegisteredCountryGeoName { get; set; }
		public CountryLocationEntity? RepresentedCountryGeoName { get; set; }
	}
}
