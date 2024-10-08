using CSharpFunctionalExtensions;
using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.Country.Commands.CreateCountryLocation
{
	public class CreateCountryLocationHandler: IRequestHandler<CreateCountryLocation, Result>
	{
		private readonly ICountryRepository _countryRepository;

		public CreateCountryLocationHandler(ICountryRepository countryRepository) => _countryRepository = countryRepository;

		public async Task<Result> Handle(CreateCountryLocation request, CancellationToken cancellationToken)
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

			return Result.Success();
		}
	}
}
