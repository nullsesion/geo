using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using MediatR;

namespace Geo.Application.CQRS.Country.Commands.MultiCreateCountryLocation
{
	public class MultiCreateCountryLocationHandler: IRequestHandler<MultiCreateCountryLocation,ResponseEntity<int>>
	{
		private readonly ICountryLocationRepository _countryLocationRepository;

		public MultiCreateCountryLocationHandler(ICountryLocationRepository countryLocationRepository) => 
			_countryLocationRepository = countryLocationRepository;

		public async Task<ResponseEntity<int>> Handle(MultiCreateCountryLocation request, CancellationToken cancellationToken)
		{
			if (request.CountryLocations.Any())
			{
				foreach (ICountryLocation countryLocation in request.CountryLocations)
				{
					await _countryLocationRepository.InsertAsync(new CountryLocation()
					{
						GeonameId         = countryLocation.GeonameId,
						LocaleCode        = countryLocation.LocaleCode,
						ContinentCode     = countryLocation.ContinentCode,
						ContinentName     = countryLocation.ContinentName,
						CountryIsoCode    = countryLocation.CountryIsoCode,
						CountryName       = countryLocation.CountryName,
						IsInEuropeanUnion = countryLocation.IsInEuropeanUnion,
					}, cancellationToken);
				}

				await _countryLocationRepository.SaveChangesAsync();
			}
			return new ResponseEntity<int>()
			{ 
				IsSuccess = false,
				ErrorDetail = "List Empty",
			};
		}
	}
}
