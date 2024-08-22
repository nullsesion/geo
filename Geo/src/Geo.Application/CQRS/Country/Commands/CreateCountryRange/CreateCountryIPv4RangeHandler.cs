using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.Country.Commands.CreateCountryRange
{
	public class CreateCountryIPv4RangeHandler : IRequestHandler<CreateCountryIPv4Range, ResponseEntity<string>>
	{
		private readonly ICountryRepository _countryRepository;

		public CreateCountryIPv4RangeHandler(ICountryRepository countryRepository) => _countryRepository = countryRepository;
		public async Task<ResponseEntity<string>> Handle(CreateCountryIPv4Range request, CancellationToken cancellationToken)
		{
			ResponseEntity<CountryIPv4Range> countryIPv4Range = CountryIPv4Range.Create(request);

			if (!countryIPv4Range.IsSuccess)
			{
				return new ResponseEntity<string>()
				{
					ErrorDetail = countryIPv4Range.ErrorDetail,
					IsSuccess = false,
				};
			}

			await _countryRepository.InsertCountryIPv4RangeAsync(countryIPv4Range.Entity, cancellationToken);

			await _countryRepository.SaveChangesAsync();

			return new ResponseEntity<string>()
			{
				Entity = request.Network,
				IsSuccess = true,
			};
		}
	}
}
