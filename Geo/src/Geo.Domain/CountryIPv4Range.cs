using Geo.DomainShared;
using Geo.DomainShared.Contracts;

namespace Geo.Domain
{
	public class CountryIPv4Range
	{
		private const string ERROR_CREATE = "Error Create Country IPv4 Range";

		public static ResponseEntity<CountryIPv4Range> Create(ICountryIPv4Range countryIPv4Range)
		{
			return Create(
				countryIPv4Range.Network,
				countryIPv4Range.GeonameId,
				countryIPv4Range.RegisteredCountryGeoNameId,
				countryIPv4Range.RepresentedCountryGeoNameId,
				countryIPv4Range.IsAnonymousProxy,
				countryIPv4Range.IsSatelliteProvider,
				countryIPv4Range.IsAnycast
			);
		}

		public static ResponseEntity<CountryIPv4Range> Create(
			string network,
			int? geonameId,
			int? registeredCountryGeoNameId,
			int? representedCountryGeoNameId,
			bool isAnonymousProxy,
			bool isSatelliteProvider,
			bool? isAnycast = null
			)
		{
			if (geonameId == null && registeredCountryGeoNameId == null && representedCountryGeoNameId == null)
			{
				return new ResponseEntity<CountryIPv4Range>()
				{
					IsSuccess = false,
					ErrorDetail = ERROR_CREATE
				};
			}
			
			if (GetFromString(network, out int mask, out int ipMin, out int ipMax))
			{
				CountryIPv4Range countryIPv4Range = new CountryIPv4Range()
				{
					Network = network,
					Mask = mask,
					IpMin = ipMin,
					IpMax = ipMax,
					GeonameId = geonameId,
					RegisteredCountryGeoNameId = registeredCountryGeoNameId,
					RepresentedCountryGeoNameId = representedCountryGeoNameId,
					IsAnonymousProxy = isAnonymousProxy,
					IsSatelliteProvider = isSatelliteProvider,
					IsAnycast = isAnycast
				};
				return new ResponseEntity<CountryIPv4Range>()
				{
					IsSuccess = true,
					Entity = countryIPv4Range,
				};
			}

			return new ResponseEntity<CountryIPv4Range>()
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

			if(1 > m || 31 < m)
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
		public int Mask { get; private set; }
		public int IpMin { get; private set; }
		public int IpMax { get; private set; }
		public int? GeonameId { get; private set; }
		public int? RegisteredCountryGeoNameId { get; private set; }
		public int? RepresentedCountryGeoNameId { get; private set; }
		public bool IsAnonymousProxy { get; private set; }
		public bool IsSatelliteProvider { get; private set; }
		public bool? IsAnycast { get; private set; }
		public ICountryLocation? Geoname { get; set; }
		public ICountryLocation? RegisteredCountryGeoName { get; set; }
		public ICountryLocation? RepresentedCountryGeoName { get; set; }
	}
}
