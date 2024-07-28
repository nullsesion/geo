using Geo.Application.Interfaces;
using Geo.DataAccess.Entities;
using Geo.Domain;
using Microsoft.EntityFrameworkCore;

namespace Geo.DataAccess.Repositories
{
	public class CountryIPv4Repository: AbstractRepository, ICountryIPv4Repository
	{
		public CountryIPv4Repository(GeoApiDbContext dbContext) : base(dbContext)
		{ }

		public async Task<bool> InsertAsync(CountryIPv4Range countryIPv4Range, CancellationToken cancellationToken)
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

		public async Task SaveChangesAsync()
		{
			await _dbContext.SaveChangesAsync();
		}
	}
}
