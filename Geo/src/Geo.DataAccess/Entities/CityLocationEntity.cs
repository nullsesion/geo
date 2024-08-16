using Geo.DomainShared.Contracts;

namespace Geo.DataAccess.Entities
{
	public class CityLocationEntity
	{
		public int GeonameId { get; set; }
		public string ContinentCode { get; set; }
		public string ContinentName { get; set; }
		public string CountryIsoCode { get; set; }
		public string CountryName { get; set; }
		public string Subdivision { get; set; }
		public string CityName { get; set; }
		public int? MetroCode { get; set; }
		public string TimeZone { get; set; }
		public bool IsInEuropeanUnion { get; set; }
	}
}
