using CSharpFunctionalExtensions;
using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.Country.Queries.GetCountry
{
	public class GetCountryHandler: IRequestHandler<GetCountry, Result<CountryIPv4Range>>
	{
		private readonly ICountryRepository _countryRepository;

		public GetCountryHandler(ICountryRepository countryRepository) 
			=> _countryRepository = countryRepository;

		public async Task<Result<CountryIPv4Range>> Handle(GetCountry request, CancellationToken cancellationToken)
		{
			return 
				await _countryRepository.GetCountryIPv4RangeByIp(request);
		}
	}
}
