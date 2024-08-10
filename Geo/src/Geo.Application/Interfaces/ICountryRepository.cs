using Geo.Domain;
using Geo.DomainShared;

namespace Geo.Application.Interfaces;

public interface ICountryRepository
{
	Task<bool> InsertCountryIPv4RangeAsync(CountryIPv4Range countryIPv4Range, CancellationToken cancellationToken);
	Task<ResponseEntity<CountryIPv4Range>> GetCountryIPv4RangeByIp(string ip);
	Task<bool> TruncateCountryIPv4RangeAsync();
	Task SaveChangesAsync();
}