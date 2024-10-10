namespace Geo.DomainShared.Contracts;

public interface ICountryIPv4Range
{
	public string Network { get; set; }
	public int? GeonameId { get; set; }
	public int? RegisteredCountryGeoNameId { get; set; }
	public int? RepresentedCountryGeoNameId { get; set; }
	public bool IsAnonymousProxy { get; set; }
	public bool IsSatelliteProvider { get; set; }
	public bool? IsAnycast { get; set; }
}