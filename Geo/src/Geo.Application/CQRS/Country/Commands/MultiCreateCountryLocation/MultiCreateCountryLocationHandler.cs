using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using MediatR;

namespace Geo.Application.CQRS.Country.Commands.MultiCreateCountryLocation
{
	public class MultiCreateCountryLocationHandler: IRequestHandler<MultiCreateCountryLocation,ResponseEntity<bool>>
	{
		private readonly ICountryRepository _countryRepository;

		public MultiCreateCountryLocationHandler(ICountryRepository countryRepository) => 
			_countryRepository = countryRepository;

		public async Task<ResponseEntity<bool>> Handle(MultiCreateCountryLocation request, CancellationToken cancellationToken)
		{
			if (request.CountryLocations.Any())
			{
				bool res = await _countryRepository.MultiInsertCountryLocationAsync(request.CountryLocations, cancellationToken);
				return new ResponseEntity<bool>()
				{
					Entity = res,
					IsSuccess = true,
				};
			}
			return new ResponseEntity<bool>()
			{ 
				IsSuccess = false,
				ErrorDetail = "List Empty",
			};
		}
	}
}
