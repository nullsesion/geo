using Geo.Domain;

namespace Geo.Application.Interfaces;

public interface ICityLocationRepository
{
	Task<int> InsertAsync(CityLocation cityLocation, CancellationToken cancellationToken);
	Task<bool> TruncateAsync();
	Task SaveChangesAsync();
}