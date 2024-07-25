namespace Geo.DataAccess.Entities
{
	public class CountryIPv4Entity
	{
		public string Network { get; set; }
		public uint IpMin { get; set; }
		public uint IpMax { get; set; }
		public int? GeonameId { get; set; }
		public int? RegisteredCountryGeoNameId { get; set; }
		public int? RepresentedCountryGeoNameId { get; set; }
		public bool IsAnonymousProxy { get; set; }
		public bool IsSatelliteProvider { get; set; }
		public bool? IsAnycast { get; set; }
	}
}
