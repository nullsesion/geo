using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using MediatR;

namespace Geo.Application.CQRS.City.Commands.CreateCityLocation
{
	public class CreateCityLocationHandler: IRequestHandler<CreateCityLocation, ResponseEntity<int>>
	{
		private readonly ICityLocationRepository _cityLocationRepository;

		public CreateCityLocationHandler(ICityLocationRepository cityLocationRepository) => _cityLocationRepository = cityLocationRepository;

		public async Task<ResponseEntity<int>> Handle(CreateCityLocation request, CancellationToken cancellationToken)
		{
			var res = await _cityLocationRepository.InsertAsync(new CityLocation()
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

			await _cityLocationRepository.SaveChangesAsync();

			return new ResponseEntity<int>()
			{
				IsSuccess = true,
				Entity = res,
			};
		}
	}
}
