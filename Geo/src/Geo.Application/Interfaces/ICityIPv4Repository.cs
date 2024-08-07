using Geo.Domain;

namespace Geo.Application.Interfaces;

public interface ICityIPv4Repository
{
	Task<bool> InsertAsync(CityIPv4Range cityIPv4Range, CancellationToken cancellationToken);
	Task<bool> TruncateAsync();
	Task SaveChangesAsync();
}