using Geo.Domain;

namespace Geo.Application.Interfaces;

public interface ICityIPv4Repository
{
	Task<bool> InsertCityIPv4RangeAsync(CityIPv4Range cityIPv4Range, CancellationToken cancellationToken);
	Task<bool> TruncateCityIPv4RangeAsync();
	Task<int> InsertCityLocationAsync(CityLocation cityLocation, CancellationToken cancellationToken);
	Task<bool> TruncateCityLocationAsync();
	Task SaveChangesAsync();
}