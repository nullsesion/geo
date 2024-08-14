using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using NpgsqlTypes;

namespace Geo.Domain
{
	public class CityIPv4Range
	{
		private const string ERROR_CREATE = "Error Create City IPv4 Range";

		private CityIPv4Range()
		{
		}

		public static ResponseEntity<CityIPv4Range> Create(ICityIPv4Range cityIPv4Range)
		{
			if (cityIPv4Range.GeonameId == null
			    && cityIPv4Range.RegisteredCountryGeoNameId == null
			    && cityIPv4Range.RepresentedCountryGeoNameId == null)
			{
				return new ResponseEntity<CityIPv4Range>()
				{
					IsSuccess = false,
					ErrorDetail = ERROR_CREATE
				};
			}

			

			if (GetFromString(cityIPv4Range.Network, out int mask, out int ipMin, out int ipMax))
			{
				CityIPv4Range _cityIPv4Range = new CityIPv4Range()
				{
						Network = cityIPv4Range.Network,
						IpMin = ipMin,
						IpMax = ipMax,
						GeonameId = cityIPv4Range.GeonameId,
						RegisteredCountryGeoNameId = cityIPv4Range.RegisteredCountryGeoNameId,
						RepresentedCountryGeoNameId = cityIPv4Range.RepresentedCountryGeoNameId,
						IsAnonymousProxy = cityIPv4Range.IsAnonymousProxy,
						IsSatelliteProvider = cityIPv4Range.IsSatelliteProvider,
						IsAnycast = cityIPv4Range.IsAnycast,
						Location  = cityIPv4Range.Location,
						AccuracyRadius = cityIPv4Range.AccuracyRadius
				};
				
				return new ResponseEntity<CityIPv4Range>()
				{
					IsSuccess = true,
					Entity = _cityIPv4Range,
				};
			}

			return new ResponseEntity<CityIPv4Range>()
			{
				IsSuccess = false,
				ErrorDetail = ERROR_CREATE
			};
		}

		private static bool GetFromString(string network, out int mask, out int ipMin, out int ipMax)
		{
			mask = ipMin = ipMax = 0;
			string[] ipAndMask = network.Split('/');
			if (ipAndMask.Length != 2)
				return false;

			if (!int.TryParse(ipAndMask[1], out int m))
				return false;

			if (1 > m || 31 < m)
				return false;

			mask = m;
			if (ipAndMask[0].TryIpV4GetMaxMinViaMask(mask, out UInt32 max, out UInt32 min))
			{
				ipMax = max.ToInt32();
				ipMin = min.ToInt32();
				return true;
			}

			return false;
		}

		public string Network { get; private set; }
		public int IpMin { get; private set; }
		public int IpMax { get; private set; }
		public int? GeonameId { get; private set; }
		public int? RegisteredCountryGeoNameId { get; private set; }
		public int? RepresentedCountryGeoNameId { get; private set; }
		public bool IsAnonymousProxy { get; private set; }
		public bool IsSatelliteProvider { get; private set; }
		public bool? IsAnycast { get; private set; } = false;
		public Coordinate? Location { get; private set; }
		public int? AccuracyRadius { get; private set; }
	}
}
