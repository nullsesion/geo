using CSharpFunctionalExtensions;
using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.City.Commands.CreateCityLocation
{
	public class CreateCityLocationHandler: IRequestHandler<CreateCityLocation, Result>
	{
		private readonly ICityIPv4Repository _cityIPv4Repository;

		public CreateCityLocationHandler(ICityIPv4Repository cityIPv4Repository) => _cityIPv4Repository = cityIPv4Repository;

		public async Task<Result> Handle(CreateCityLocation request, CancellationToken cancellationToken)
		{
			var res = await _cityIPv4Repository.InsertCityLocationAsync(new CityLocation()
			{
				GeonameId = request.GeonameId,
				LocaleCode = request.LocaleCode,
				ContinentCode = request.ContinentCode,
				ContinentName = request.ContinentName,
				CountryIsoCode = request.CountryIsoCode,
				CountryName = request.CountryName,
				Subdivision1IsoCode = request.Subdivision1IsoCode,
				Subdivision1Name = request.Subdivision1Name,
				Subdivision2IsoCode = request.Subdivision2IsoCode,
				Subdivision2Name = request.Subdivision2Name,
				CityName = request.CityName,
				MetroCode = request.MetroCode,
				TimeZone = request.TimeZone,
				IsInEuropeanUnion = request.IsInEuropeanUnion,
			}, cancellationToken);

			await _cityIPv4Repository.SaveChangesAsync();

			return Result.Success();
		}
	}
}
