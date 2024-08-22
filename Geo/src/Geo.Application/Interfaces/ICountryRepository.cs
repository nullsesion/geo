using Geo.Application.CQRS.Country.Queries.GetCountry;
using Geo.Domain;
using Geo.DomainShared;

namespace Geo.Application.Interfaces;

public interface ICountryRepository
{
	Task<bool> InsertCountryIPv4RangeAsync(CountryIPv4Range countryIPv4Range, CancellationToken cancellationToken);
	Task<ResponseEntity<CountryIPv4Range>> GetCountryIPv4RangeByIp(GetCountry ip);
	Task<bool> TruncateCountryIPv4RangeAsync();
	Task<int> InsertCountryLocationAsync(CountryLocation countryLocation, CancellationToken cancellationToken);
	Task<bool> TruncateCountryLocationAsync();
	Task SaveChangesAsync();
}