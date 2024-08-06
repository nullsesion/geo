using Geo.Application.Interfaces;
using Geo.DataAccess.Configuration;
using Geo.Domain;
using GeoLoad.Entities;

namespace Geo.DataAccess.Repositories
{

	public class CityIPv4Repository: AbstractRepository, ICityIPv4Repository
	{
		public CityIPv4Repository(GeoApiDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<bool> InsertAsync(CityIPv4Range cityIPv4Range, CancellationToken cancellationToken)
		{
			var res = await _dbContext
					.CityIPv4s
					.AddAsync(new CityIPv4Entity()
					{
						Network = cityIPv4Range.Network,
						IpMin = cityIPv4Range.IpMin,
						IpMax = cityIPv4Range.IpMax,
						GeonameId = cityIPv4Range.GeonameId,
						RegisteredCountryGeoNameId = cityIPv4Range.RegisteredCountryGeoNameId,
						RepresentedCountryGeoNameId = cityIPv4Range.RepresentedCountryGeoNameId,
						IsAnonymousProxy = cityIPv4Range.IsAnonymousProxy,
						IsSatelliteProvider = cityIPv4Range.IsSatelliteProvider,
						IsAnycast = cityIPv4Range.IsAnycast,
						Location = cityIPv4Range.Location,
						AccuracyRadius = cityIPv4Range.AccuracyRadius,
					})
				;

			return true;
		}

		public async Task<bool> Truncate()
		{
			await _dbContext
				.CityIPv4s
				.Truncate();

			return true;
		}

		public async Task SaveChangesAsync()
		{
			await _dbContext.SaveChangesAsync();
		}
	}
}
