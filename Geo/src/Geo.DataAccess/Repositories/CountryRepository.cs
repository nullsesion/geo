using AutoMapper;
using EFCore.BulkExtensions;
using Geo.Application.CQRS.Country.Queries.GetCountry;
using Geo.Application.Interfaces;
using Geo.DataAccess.Configuration;
using Geo.DataAccess.Entities;
using Geo.Domain;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace Geo.DataAccess.Repositories
{
	public class CountryRepository: AbstractRepository, ICountryRepository
	{
		private readonly IMapper _mapper;

		public CountryRepository(GeoApiDbContext dbContext, IMapper mapper) : base(dbContext)
		{
			_mapper = mapper;
		}

		public async Task<bool> InsertCountryIPv4RangeAsync(CountryIPv4Range countryIPv4Range, CancellationToken cancellationToken)
		{
			var res = await _dbContext
					.CountryIPv4s  
					.AddAsync(_mapper.Map<CountryIPv4Entity>(countryIPv4Range))
				;

			return true;
		}

		public async Task<ResponseEntity<CountryIPv4Range>> GetCountryIPv4RangeByIp(GetCountry ip)
		{

			if (ip.Ip.TryIpV4ToInt(out int number))
			{
				//todo: use linq2db
				/*
				var list = await _dbContext
					.CountryIPv4s
					.Where(x =>
						x.IpMin < number && x.IpMax > number ||
						x.IpMin > number && x.IpMax < number
					)
					.SelectMany(
						countryIPv4S => _dbContext.CountryLocations
							.Where(o =>
								o.GeonameId == countryIPv4S.GeonameId
								|| o.GeonameId == countryIPv4S.RegisteredCountryGeoNameId
								|| o.GeonameId == countryIPv4S.RepresentedCountryGeoNameId
							).DefaultIfEmpty()
						, (countryIPv4S, countryLocation)
							=> new { countryIPv4S, countryLocation }
					).ToListAsync();
				*/
				
				CountryIPv4Entity? countryIPv4s = await _dbContext
					.CountryIPv4s
					.Include(x=>x.Geoname)
					.Include(x => x.RegisteredCountryGeoName)
					.Include(x => x.RepresentedCountryGeoName)
					.FirstOrDefaultAsync( x =>
						x.IpMin < number && x.IpMax > number 
						|| x.IpMin > number && x.IpMax < number
					)
				;

				if (countryIPv4s == null)
					return new ResponseEntity<CountryIPv4Range>()
					{
						IsSuccess = false,
						ErrorDetail = "Not Found",
					};

				ResponseEntity<CountryIPv4Range> entity = CountryIPv4Range.Create(countryIPv4s);

				if (!entity.IsSuccess)
				{
					return new ResponseEntity<CountryIPv4Range>()
					{
						IsSuccess = false,
						ErrorDetail = entity.ErrorDetail,
					};
				}
					
				entity.Entity
					.SetGeoname(countryIPv4s.Geoname == null
						? null
						:_mapper.Map<CountryLocation>(countryIPv4s.Geoname))
				.SetRegisteredCountryGeoName(countryIPv4s.RegisteredCountryGeoName == null
						? null
						:_mapper.Map<CountryLocation>(countryIPv4s.RegisteredCountryGeoName))
				.SetRepresentedCountryGeoName(countryIPv4s.RepresentedCountryGeoName == null
						? null
						: _mapper.Map<CountryLocation>(countryIPv4s.RepresentedCountryGeoName))
					;
				
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
					.AddAsync(_mapper.Map<CountryLocationEntity>(countryLocation)
						, cancellationToken)
				;

			return countryLocation.GeonameId;
		}

		public async Task<bool> MultiInsertCountryLocationAsync(IEnumerable<ICountryLocation> countryLocations, CancellationToken cancellationToken)
		{
			try
			{
				await _dbContext.TruncateAsync<CountryLocationEntity>();
				await _dbContext.BulkInsertAsync(countryLocations.Select(x => _mapper.Map<CountryLocationEntity>(x)));
				_dbContext.BulkSaveChanges();
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
		}

		public async Task<bool> MultiInsertCountryIPv4RangeAsync(IEnumerable<ICountryIPv4Range> countryIPv4Ranges, CancellationToken cancellationToken)
		{
			try
			{
				await _dbContext.TruncateAsync<CountryIPv4Entity>();
				await _dbContext.BulkInsertAsync(countryIPv4Ranges.Select(x => _mapper.Map<CountryIPv4Entity>(x)));
				_dbContext.BulkSaveChanges();
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
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
