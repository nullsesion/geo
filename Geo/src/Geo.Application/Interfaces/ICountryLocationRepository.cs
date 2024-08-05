using Geo.Domain;
namespace Geo.Application.Interfaces;
public interface ICountryLocationRepository
{
	Task<int> InsertAsync(CountryLocation countryLocationEntity, CancellationToken cancellationToken);
	Task<bool> Truncate();
	Task SaveChangesAsync();
}