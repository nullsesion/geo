using Geo.Application.Interfaces;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.City.Commands.MultiCreateCityLocation
{
	public class MultiCreateCityLocationHandler: IRequestHandler<MultiCreateCityLocation, ResponseEntity<bool>>
	{
		private readonly ICityIPv4Repository _cityIPv4Repository;

		public MultiCreateCityLocationHandler(ICityIPv4Repository cityIPv4Repository) => _cityIPv4Repository = cityIPv4Repository;

		public async Task<ResponseEntity<bool>> Handle(MultiCreateCityLocation request, CancellationToken cancellationToken)
		{
			bool res = _cityIPv4Repository.MultiInsertCityLocationAsync(request.CityLocations, cancellationToken);
			return new ResponseEntity<bool>()
			{
				IsSuccess = true,
				Entity = res,
			};
		}
	}
}
