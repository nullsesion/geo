using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using MediatR;

namespace Geo.Application.CQRS.City.Commands.MultiCreateCityLocation
{
	public class MultiCreateCityLocationHandler: IRequestHandler<MultiCreateCityLocation, ResponseEntity<IEnumerable<int>>>
	{
		private readonly ICityIPv4Repository _cityIPv4Repository;

		public MultiCreateCityLocationHandler(ICityIPv4Repository cityIPv4Repository) => _cityIPv4Repository = cityIPv4Repository;

		public async Task<ResponseEntity<IEnumerable<int>>> Handle(MultiCreateCityLocation request, CancellationToken cancellationToken)
		{
			List<int> listResult = new List<int>();
			foreach (ICityLocation cityLocation in request.CityLocations)
			{
				//cityLocation
				var id = await _cityIPv4Repository.InsertCityLocationAsync(new CityLocation()
				{
					GeonameId = cityLocation.GeonameId,
					LocaleCode = cityLocation.LocaleCode,
					ContinentCode = cityLocation.ContinentCode,
					ContinentName = cityLocation.ContinentName,
					CountryIsoCode = cityLocation.CountryIsoCode,
					CountryName = cityLocation.CountryName,
					Subdivision1IsoCode = cityLocation.Subdivision1IsoCode,
					Subdivision1Name = cityLocation.Subdivision2Name,
					Subdivision2IsoCode = cityLocation.Subdivision2IsoCode,
					Subdivision2Name = cityLocation.Subdivision2Name,
					CityName = cityLocation.CityName,
					MetroCode = cityLocation.MetroCode,
					TimeZone = cityLocation.TimeZone,
					IsInEuropeanUnion = cityLocation.IsInEuropeanUnion,
				}, cancellationToken);
				listResult.Add(id);
			}

			await _cityIPv4Repository.SaveChangesAsync();
			return new ResponseEntity<IEnumerable<int>>()
			{
				IsSuccess = true,
				Entity = listResult
			};
		}
	}
}
