using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using MediatR;

namespace Geo.Application.CQRS.Country.Commands.MultiCreateCountryRange
{
	public class MultiCreateCountryRangeHandler: IRequestHandler<MultiCreateCountryRangeIRequest, ResponseEntity<bool>>
	{
		private readonly ICountryRepository _countryRepository;

		public MultiCreateCountryRangeHandler(ICountryRepository countryRepository) =>
			_countryRepository = countryRepository;
		public async Task<ResponseEntity<bool>> Handle(MultiCreateCountryRangeIRequest request, CancellationToken cancellationToken)
		{
			if (request.CountryIPv4Ranges.Any())
			{
				await _countryRepository.MultiInsertCountryIPv4RangeAsync(request.CountryIPv4Ranges, cancellationToken);
				return new ResponseEntity<bool>()
				{
					Entity = true,
					IsSuccess = true,
				};
			}
			return new ResponseEntity<bool>()
			{
				ErrorDetail = "Empty List",
				IsSuccess = false,
			};
		}
	}
}
