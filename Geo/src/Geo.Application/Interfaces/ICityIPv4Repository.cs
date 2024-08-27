using Geo.Application.CQRS.City.Queries;
using Geo.Domain;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;

namespace Geo.Application.Interfaces;

public interface ICityIPv4Repository
{
	Task<bool> InsertCityIPv4RangeAsync(CityIPv4Range cityIPv4Range, CancellationToken cancellationToken);
	Task<bool> TruncateCityIPv4RangeAsync();
	Task<int> InsertCityLocationAsync(CityLocation cityLocation, CancellationToken cancellationToken);
	Task<bool> TruncateCityLocationAsync();
	Task<ResponseEntity<CityIPv4Range>> GetCityIPv4RangeByIp(GetCity ip);

	bool MultiInsertCityLocationAsync(IEnumerable<ICityLocation> cityLocations,
		CancellationToken cancellationToken);

	Task<bool> MultiInsertCityIPv4RangeAsync(IEnumerable<ICityIPv4Range> cityIPv4Ranges,
		CancellationToken cancellationToken);

	Task SaveChangesAsync();
}