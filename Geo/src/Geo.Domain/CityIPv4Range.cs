using CSharpFunctionalExtensions;
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

		
		public static Result<CityIPv4Range> Create(string network
															,int? geonameId
															,int? registeredCountryGeoNameId
															,int? representedCountryGeoNameId
															,bool isAnonymousProxy
															,bool isSatelliteProvider
															,bool? isAnycast
															,Coordinate? location
															,int? accuracyRadius)
		{
			if (geonameId == null
			    && registeredCountryGeoNameId == null
			    && representedCountryGeoNameId == null)
			{
				return Result.Failure<CityIPv4Range>(ERROR_CREATE);
			}



			if (GetFromString(network, out int mask, out int ipMin, out int ipMax))
			{
				CityIPv4Range _cityIPv4Range = new CityIPv4Range()
				{
					Network = network,
					IpMin = ipMin,
					IpMax = ipMax,
					GeonameId = geonameId,
					RegisteredCountryGeoNameId = registeredCountryGeoNameId,
					RepresentedCountryGeoNameId = representedCountryGeoNameId,
					IsAnonymousProxy = isAnonymousProxy,
					IsSatelliteProvider = isSatelliteProvider,
					IsAnycast = isAnycast,
					Location = location,
					AccuracyRadius = accuracyRadius
				};

				return Result.Success(_cityIPv4Range);
			}

			return Result.Failure<CityIPv4Range>(ERROR_CREATE);
		}
		
		public static Result<CityIPv4Range> Create(ICityIPv4Range cityIPv4Range)
		{
			if (cityIPv4Range.GeonameId == null
			    && cityIPv4Range.RegisteredCountryGeoNameId == null
			    && cityIPv4Range.RepresentedCountryGeoNameId == null)
			{
				return Result.Failure<CityIPv4Range>(ERROR_CREATE);
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
						//Location  = cityIPv4Range.Location,
						AccuracyRadius = cityIPv4Range.AccuracyRadius
				};

				return Result.Success(_cityIPv4Range);
			}

			return Result.Failure<CityIPv4Range>(ERROR_CREATE);
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

		public CityIPv4Range SetGeoname(ICityLocation? geoname)
		{
			Geoname = geoname;
			return this;
		}
		public CityIPv4Range SetRegisteredCountryGeoName(ICityLocation? registeredCountryGeoName)
		{
			RegisteredCountryGeoName = registeredCountryGeoName;
			return this;
		}
		public CityIPv4Range SetRepresentedCountryGeoName(ICityLocation? representedCountryGeoName)
		{
			RepresentedCountryGeoName = representedCountryGeoName;
			return this;
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
		public ICityLocation? Geoname { get; private set; }
		public ICityLocation? RegisteredCountryGeoName { get; private set; }
		public ICityLocation? RepresentedCountryGeoName { get; private set; }
	}
}
