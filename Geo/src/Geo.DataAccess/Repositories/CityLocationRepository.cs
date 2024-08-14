using AutoMapper;
using Geo.Application.Interfaces;
using Geo.DataAccess.Configuration;
using Geo.DataAccess.Entities;
using Geo.Domain;
using Newtonsoft.Json;

namespace Geo.DataAccess.Repositories
{
	public class CityLocationRepository: AbstractRepository, ICityLocationRepository
	{
		private readonly IMapper _mapper;

		public CityLocationRepository(GeoApiDbContext dbContext, IMapper mapper) : base(dbContext)
		{
			_mapper = mapper;
		}

		public async Task<int> InsertAsync(CityLocation cityLocation, CancellationToken cancellationToken)
		{
			var res = await _dbContext
				.CityLocations
				.AddAsync(_mapper.Map<CityLocationEntity>(cityLocation), cancellationToken);

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
