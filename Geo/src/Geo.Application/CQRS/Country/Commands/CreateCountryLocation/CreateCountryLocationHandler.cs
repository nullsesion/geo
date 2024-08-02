using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.Country.Commands.CreateCountryLocation
{
	public class CreateCountryLocationHandler: IRequestHandler<CreateCountryLocation, ResponseEntity<int>>
	{
		private readonly ICountryLocationRepository _countryLocationRepository;

		public CreateCountryLocationHandler(ICountryLocationRepository countryLocationRepository) => _countryLocationRepository = countryLocationRepository;

		public async Task<ResponseEntity<int>> Handle(CreateCountryLocation request, CancellationToken cancellationToken)
		{
			CountryLocation countryLocation = new CountryLocation()
			{
				GeonameId = request.GeonameId,
				ContinentCode = request.ContinentCode,
				ContinentName = request.ContinentName,
				CountryIsoCode = request.CountryIsoCode,
				CountryName = request.CountryName,
				IsInEuropeanUnion = request.IsInEuropeanUnion,
			};

			int geonameId = await _countryLocationRepository.InsertAsync(countryLocation, cancellationToken);

			await _countryLocationRepository.SaveChangesAsync();

			return new ResponseEntity<int>()
			{
				Entity = geonameId,
				IsSuccess = true,
			};
		}
	}
}
