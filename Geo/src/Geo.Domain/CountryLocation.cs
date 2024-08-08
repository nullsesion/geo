using Geo.DomainShared.Contracts;

namespace Geo.Domain
{
	//todo: add validator, creator and private setters.
	public class CountryLocation : ICountryLocation
	{
		public int GeonameId { get; set; }
		public string LocaleCode { get; set; }
		public string ContinentCode { get; set; }
		public string ContinentName { get; set; }
		public string CountryIsoCode { get; set; }
		public string CountryName { get; set; }
		public bool IsInEuropeanUnion { get; set; }
	}
}
