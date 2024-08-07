using Geo.Application.Interfaces;
using Geo.DataAccess.Configuration;
using Geo.DataAccess.Entities;
using Geo.Domain;
using Newtonsoft.Json;

namespace Geo.DataAccess.Repositories
{
	public class CityLocationRepository: AbstractRepository, ICityLocationRepository
	{
		public CityLocationRepository(GeoApiDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<int> InsertAsync(CityLocation cityLocation, CancellationToken cancellationToken)
		{
			//throw new NotImplementedException();
			var res = await _dbContext
				.CityLocations
				.AddAsync(new CityLocationEntity()
				{
					GeonameId = cityLocation.GeonameId,
					LocaleCode = cityLocation.LocaleCode,
					ContinentCode = cityLocation.ContinentCode,
					ContinentName = cityLocation.ContinentName,
					CountryIsoCode = cityLocation.CountryIsoCode,
					CountryName = cityLocation.CountryName,
					Subdivision = JsonConvert.SerializeObject(new Dictionary<string, string>()
						{
							{ "Subdivision1IsoCode", cityLocation.Subdivision1IsoCode },
							{ "Subdivision1Name",    cityLocation.Subdivision1Name },
							{ "Subdivision2IsoCode", cityLocation.Subdivision2IsoCode },
							{ "Subdivision2Name",    cityLocation.Subdivision2Name },
						}
					),
					CityName = cityLocation.CityName,
					MetroCode = cityLocation.MetroCode,
					TimeZone = cityLocation.TimeZone,
					IsInEuropeanUnion = cityLocation.IsInEuropeanUnion,
				}, cancellationToken);

			return cityLocation.GeonameId;
		}

		public async Task<bool> TruncateAsync()
		{
			await _dbContext
				.CityLocations
				.Truncate();

			return true;
		}
		public async Task SaveChangesAsync()
		{
			await _dbContext.SaveChangesAsync();
		}
	}
}
