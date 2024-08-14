using Geo.DomainShared.Contracts;
using NpgsqlTypes;

namespace GeoLoad.Entities
{
	public class CityIPv4Entity
	{	
		public string Network { get; set; }
		public int IpMin { get; set; }
		public int IpMax { get; set; }
		public int? GeonameId { get; set; }
		public int? RegisteredCountryGeoNameId { get; set; }
		public int? RepresentedCountryGeoNameId { get; set; }
		public bool IsAnonymousProxy { get; set; }
		public bool IsSatelliteProvider { get; set; }
		public bool? IsAnycast { get; set; } = false;
		public NpgsqlPoint? Location { get; set; }
		public int? AccuracyRadius { get; set; }

	}
}
