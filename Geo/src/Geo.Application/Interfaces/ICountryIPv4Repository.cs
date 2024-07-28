using Geo.Domain;
namespace Geo.Application.Interfaces;

public interface ICountryIPv4Repository
{
	Task<bool> InsertAsync(CountryIPv4Range countryIPv4Range, CancellationToken cancellationToken);
	Task SaveChangesAsync();
}