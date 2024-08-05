using Geo.Application.Interfaces;
using Geo.DataAccess.Configuration;
using Geo.DataAccess.Entities;
using Geo.Domain;
using Geo.DomainShared.Contracts;
using Newtonsoft.Json;

namespace Geo.DataAccess.Repositories
{
	public class CountryLocationRepository: AbstractRepository, ICountryLocationRepository
	{
		public CountryLocationRepository(GeoApiDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<int> InsertAsync(CountryLocation countryLocationEntity, CancellationToken cancellationToken)
		{
			var res = await _dbContext
					.CountryLocations
					.AddAsync(new CountryLocationEntity()
					{
						GeonameId = countryLocationEntity.GeonameId,
						ContinentCode = countryLocationEntity.ContinentCode,
						ContinentName = JsonConvert.SerializeObject(new Dictionary<string, string>()
								{{countryLocationEntity.LocaleCode, countryLocationEntity.ContinentName}}
						),
						CountryIsoCode = countryLocationEntity.CountryIsoCode,
						CountryName = JsonConvert.SerializeObject(new Dictionary<string, string>()
							{{countryLocationEntity.LocaleCode, countryLocationEntity.CountryName}}
						),
						IsInEuropeanUnion = countryLocationEntity.IsInEuropeanUnion,
					}
					, cancellationToken)
				;

			return countryLocationEntity.GeonameId;
		}

		public async Task<bool> Truncate()
		{
			await _dbContext
				.CountryLocations
				.Truncate();

			return true;
		}

		public async Task SaveChangesAsync()
		{
			await _dbContext.SaveChangesAsync();
		}
		

	}
}
