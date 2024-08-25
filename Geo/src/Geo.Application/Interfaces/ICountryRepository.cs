using Geo.Application.CQRS.Country.Queries.GetCountry;
using Geo.Domain;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;

namespace Geo.Application.Interfaces;

public interface ICountryRepository
{
	Task<bool> InsertCountryIPv4RangeAsync(CountryIPv4Range countryIPv4Range, CancellationToken cancellationToken);
	Task<ResponseEntity<CountryIPv4Range>> GetCountryIPv4RangeByIp(GetCountry ip);
	Task<bool> TruncateCountryIPv4RangeAsync();
	Task<int> InsertCountryLocationAsync(CountryLocation countryLocation, CancellationToken cancellationToken);

	Task<bool> MultiInsertCountryLocationAsync(IEnumerable<ICountryLocation> cityLocations,
		CancellationToken cancellationToken);

	Task<bool> MultiInsertCountryIPv4RangeAsync(IEnumerable<ICountryIPv4Range> cityIPv4Ranges,
		CancellationToken cancellationToken);

	Task<bool> TruncateCountryLocationAsync();
	Task SaveChangesAsync();
}