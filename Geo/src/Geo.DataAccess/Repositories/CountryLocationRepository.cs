using Geo.Application.Interfaces;
using Geo.DataAccess.Configuration;
using Geo.DataAccess.Entities;
using Geo.Domain;
using Newtonsoft.Json;

namespace Geo.DataAccess.Repositories
{
	public class CountryLocationRepository: AbstractRepository, ICountryLocationRepository
	{
		public CountryLocationRepository(GeoApiDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<int> InsertAsync(CountryLocation countryLocation, CancellationToken cancellationToken)
		{
			var res = await _dbContext
					.CountryLocations
					.AddAsync(new CountryLocationEntity()
					{
						GeonameId = countryLocation.GeonameId,
						ContinentCode = countryLocation.ContinentCode,
						ContinentName = JsonConvert.SerializeObject(new Dictionary<string, string>() {{countryLocation.LocaleCode, countryLocation.ContinentName}} ),
						CountryIsoCode = countryLocation.CountryIsoCode,
						CountryName = JsonConvert.SerializeObject(new Dictionary<string, string>() {{countryLocation.LocaleCode, countryLocation.CountryName}} ),
						IsInEuropeanUnion = countryLocation.IsInEuropeanUnion,
					}
					, cancellationToken)
				;

			return countryLocation.GeonameId;
		}

		public async Task<bool> TruncateAsync()
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
