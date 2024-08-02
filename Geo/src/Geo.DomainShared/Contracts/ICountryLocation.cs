namespace Geo.DomainShared.Contracts;

public interface ICountryLocation
{
	public int GeonameId { get; set; }
	public string ContinentCode { get; set; }
	public string ContinentName { get; set; }
	public string CountryIsoCode { get; set; }
	public string CountryName { get; set; }
	public bool IsInEuropeanUnion { get; set; }
}