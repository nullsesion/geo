using AutoMapper;
using Geo.Application.Interfaces;
using Geo.DataAccess.Configuration;
using Geo.Domain;
using GeoLoad.Entities;

namespace Geo.DataAccess.Repositories
{

	public class CityIPv4Repository: AbstractRepository, ICityIPv4Repository
	{
		private readonly IMapper _mapper;

		public CityIPv4Repository(GeoApiDbContext dbContext, IMapper mapper) : base(dbContext)
		{
			_mapper = mapper;
		}

		public async Task<bool> InsertAsync(CityIPv4Range cityIPv4Range, CancellationToken cancellationToken)
		{
			var res = await _dbContext
					.CityIPv4s
					.AddAsync(_mapper.Map<CityIPv4Entity>(cityIPv4Range))
				;

			return true;
		}

		public async Task<bool> TruncateAsync()
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
