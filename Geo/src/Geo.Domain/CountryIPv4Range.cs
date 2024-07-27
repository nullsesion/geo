namespace Geo.Domain
{
	public class CountryIPv4Range
	{
		public string Network { get; set; }
		public int IpMask { get; set; }
		public int IpMin { get; set; }
		public int IpMax { get; set; }
		public int? GeonameId { get; set; }
		public int? RegisteredCountryGeoNameId { get; set; }
		public int? RepresentedCountryGeoNameId { get; set; }
		public bool IsAnonymousProxy { get; set; }
		public bool IsSatelliteProvider { get; set; }
		public bool? IsAnycast { get; set; }
	}
}
