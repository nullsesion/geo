using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.Country.Commands.CreateCountryLocation
{
	public class CreateCountryLocationHandler: IRequestHandler<CreateCountryLocation, ResponseEntity<int>>
	{
		private readonly ICountryRepository _countryRepository;

		public CreateCountryLocationHandler(ICountryRepository countryRepository) => _countryRepository = countryRepository;

		public async Task<ResponseEntity<int>> Handle(CreateCountryLocation request, CancellationToken cancellationToken)
		{
			CountryLocation countryLocation = new CountryLocation()
			{
				GeonameId = request.GeonameId,
				LocaleCode = request.LocaleCode,
				ContinentCode = request.ContinentCode,
				ContinentName = request.ContinentName,
				CountryIsoCode = request.CountryIsoCode,
				CountryName = request.CountryName,
				IsInEuropeanUnion = request.IsInEuropeanUnion,
			};

			int geonameId = await _countryRepository.InsertCountryLocationAsync(countryLocation, cancellationToken);

			await _countryRepository.SaveChangesAsync();

			return new ResponseEntity<int>()
			{
				Entity = geonameId,
				IsSuccess = true,
			};
		}
	}
}
