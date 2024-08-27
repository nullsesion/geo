using AutoMapper;
using EFCore.BulkExtensions;
using Geo.Application.CQRS.City.Queries;
using Geo.Application.Interfaces;
using Geo.DataAccess.Configuration;
using Geo.DataAccess.Entities;
using Geo.Domain;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using GeoLoad.Entities;
using Microsoft.EntityFrameworkCore;

namespace Geo.DataAccess.Repositories
{

	public class CityIPv4Repository: AbstractRepository, ICityIPv4Repository
	{
		private readonly IMapper _mapper;

		public CityIPv4Repository(GeoApiDbContext dbContext, IMapper mapper) : base(dbContext)
		{
			_mapper = mapper;
		}

		public async Task<bool> InsertCityIPv4RangeAsync(CityIPv4Range cityIPv4Range, CancellationToken cancellationToken)
		{
			var res = await _dbContext
					.CityIPv4s
					.AddAsync(_mapper.Map<CityIPv4Entity>(cityIPv4Range))
				;

			return true;
		}

		public async Task<bool> TruncateCityIPv4RangeAsync()
		{
			await _dbContext
				.CityIPv4s
				.Truncate();

			return true;
		}

		public async Task<int> InsertCityLocationAsync(CityLocation cityLocation, CancellationToken cancellationToken)
		{
			var res = await _dbContext
				.CityLocations
				.AddAsync(_mapper.Map<CityLocationEntity>(cityLocation), cancellationToken);

			return cityLocation.GeonameId;
		}

		public async Task<bool> TruncateCityLocationAsync()
		{
			await _dbContext
				.CityLocations
				.Truncate();

			return true;
		}
		public async Task<ResponseEntity<CityIPv4Range>> GetCityIPv4RangeByIp(GetCity ip)
		{
			if (ip.Ip.TryIpV4ToInt(out int number))
			{
				CityIPv4Entity? cityIPv4s = await _dbContext
					.CityIPv4s
					.Include(x => x.Geoname)
					.Include(x => x.RegisteredCountryGeoName)
					.Include(x => x.RepresentedCountryGeoName)
					.FirstOrDefaultAsync(x =>
							x.IpMin < number && x.IpMax > number
							|| x.IpMin > number && x.IpMax < number
						)
					;

				if (cityIPv4s == null)
					return new ResponseEntity<CityIPv4Range>()
					{
						IsSuccess = false,
						ErrorDetail = "Not Found",
					};

				ResponseEntity<CityIPv4Range> entity = CityIPv4Range.Create(cityIPv4s.Network
					, cityIPv4s.GeonameId
					, cityIPv4s.RegisteredCountryGeoNameId
					, cityIPv4s.RepresentedCountryGeoNameId
					, cityIPv4s.IsAnonymousProxy
					, cityIPv4s.IsSatelliteProvider
					, cityIPv4s.IsAnycast
					, new Coordinate(cityIPv4s.Location.Value.X, cityIPv4s.Location.Value.Y)
					, cityIPv4s.AccuracyRadius);

				if (!entity.IsSuccess)
				{
					return new ResponseEntity<CityIPv4Range>()
					{
						IsSuccess = false,
						ErrorDetail = entity.ErrorDetail,
					};
				}

				entity.Entity
					.SetGeoname(cityIPv4s.Geoname == null
						? null
						: _mapper.Map<CityLocation>(cityIPv4s.Geoname))
					.SetRegisteredCountryGeoName(cityIPv4s.RegisteredCountryGeoName == null
						? null
						: _mapper.Map<CityLocation>(cityIPv4s.RegisteredCountryGeoName))
					.SetRepresentedCountryGeoName(cityIPv4s.RepresentedCountryGeoName == null
						? null
						: _mapper.Map<CityLocation>(cityIPv4s.RepresentedCountryGeoName))
					;

				return new ResponseEntity<CityIPv4Range>()
				{
					IsSuccess = true,
					Entity = entity.Entity
				};
			}

			return new ResponseEntity<CityIPv4Range>()
			{
				IsSuccess = false,
				ErrorDetail = "Bad IP",
			};
		}

		public bool MultiInsertCityLocationAsync(IEnumerable<ICityLocation> cityLocations, CancellationToken cancellationToken)
		{
			_dbContext.BulkInsertOrUpdateAsync(cityLocations.Select(x => _mapper.Map<CityLocationEntity>(x))).Wait();
			_dbContext.BulkSaveChanges();
			return true;
		}

		public async Task<bool> MultiInsertCityIPv4RangeAsync(IEnumerable<CityIPv4Range> cityIPv4Ranges, CancellationToken cancellationToken)
		{
			
			await _dbContext.BulkInsertAsync(cityIPv4Ranges.Select(x => _mapper.Map<CityIPv4Entity>(x)));
			_dbContext.BulkSaveChanges();
			
			return true;
		}

		public async Task SaveChangesAsync()
		{
			await _dbContext.SaveChangesAsync();
		}
	}
}
