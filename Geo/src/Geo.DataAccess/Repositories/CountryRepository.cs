using System.Linq.Expressions;
using Geo.Application.Interfaces;
using Geo.DataAccess.Configuration;
using Geo.DataAccess.Entities;
using Geo.Domain;
using Geo.DomainShared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Geo.DataAccess.Repositories
{
	public class CountryRepository: AbstractRepository, ICountryRepository
	{
		public CountryRepository(GeoApiDbContext dbContext) : base(dbContext)
		{ }

		public async Task<bool> InsertCountryIPv4RangeAsync(CountryIPv4Range countryIPv4Range, CancellationToken cancellationToken)
		{
			var res = await _dbContext
					.CountryIPv4s
					.AddAsync(new CountryIPv4Entity()
					{
						Network = countryIPv4Range.Network,
						IpMin = countryIPv4Range.IpMin,
						IpMax = countryIPv4Range.IpMax,
						GeonameId = countryIPv4Range.GeonameId,
						RegisteredCountryGeoNameId = countryIPv4Range.RegisteredCountryGeoNameId,
						RepresentedCountryGeoNameId = countryIPv4Range.RepresentedCountryGeoNameId,
						IsAnonymousProxy = countryIPv4Range.IsAnonymousProxy,
						IsSatelliteProvider = countryIPv4Range.IsSatelliteProvider,
						IsAnycast = countryIPv4Range.IsAnycast,
					})
				;

			return true;
		}

		public async Task<ResponseEntity<CountryIPv4Range>> GetCountryIPv4RangeByIp(string ip)
		{
			if (ip.TryIpV4ToInt(out int number))
			{
				CountryIPv4Entity? countryIPv4s = await _dbContext
					.CountryIPv4s
					.FirstOrDefaultAsync( x =>
						x.IpMin < number && x.IpMax > number 
						|| x.IpMin > number && x.IpMax < number
					);
				if (countryIPv4s == null)
					return new ResponseEntity<CountryIPv4Range>()
					{
						IsSuccess = false,
						ErrorDetail = "Not Found",
					};

				ResponseEntity<CountryIPv4Range> entity = CountryIPv4Range.Create(
					countryIPv4s.Network,
					countryIPv4s.GeonameId,
					countryIPv4s.RegisteredCountryGeoNameId,
					countryIPv4s.RepresentedCountryGeoNameId,
					countryIPv4s.IsAnonymousProxy,
					countryIPv4s.IsSatelliteProvider,
					countryIPv4s.IsAnycast
				);

				if(!entity.IsSuccess)
					return new ResponseEntity<CountryIPv4Range>()
					{
						IsSuccess = false,
						ErrorDetail = entity.ErrorDetail,
					};

				return new ResponseEntity<CountryIPv4Range>()
				{
					IsSuccess = true,
					Entity = entity.Entity
				};
			}

			return new ResponseEntity<CountryIPv4Range>()
			{
				IsSuccess = false,
				ErrorDetail = "Bad IP",
			};
		}

		public async Task<int> InsertCountryLocationAsync(CountryLocation countryLocation, CancellationToken cancellationToken)
		{
			var res = await _dbContext
					.CountryLocations
					.AddAsync(new CountryLocationEntity()
						{
							GeonameId = countryLocation.GeonameId,
							ContinentCode = countryLocation.ContinentCode,
							ContinentName = JsonConvert.SerializeObject(new Dictionary<string, string>() { { countryLocation.LocaleCode, countryLocation.ContinentName } }),
							CountryIsoCode = countryLocation.CountryIsoCode,
							CountryName = JsonConvert.SerializeObject(new Dictionary<string, string>() { { countryLocation.LocaleCode, countryLocation.CountryName } }),
							IsInEuropeanUnion = countryLocation.IsInEuropeanUnion,
						}
						, cancellationToken)
				;

			return countryLocation.GeonameId;
		}

		public async Task<bool> TruncateCountryLocationAsync()
		{
			await _dbContext
				.CountryLocations
				.Truncate();

			return true;
		}

		public async Task<bool> TruncateCountryIPv4RangeAsync()
		{
			await _dbContext
				.CountryIPv4s
				.Truncate();

			return true;
		}

		public async Task SaveChangesAsync()
		{
			await _dbContext.SaveChangesAsync();
		}
	}
}
