using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using MediatR;

namespace Geo.Application.CQRS.City.Commands.MultiCreateCityIPv4Range
{
	public class MultiCreateCityIPv4RangeHandler: IRequestHandler<MultiCreateCityIPv4Range, ResponseEntity<bool>>
	{
		private readonly ICityIPv4Repository _cityIPv4Repository;

		public MultiCreateCityIPv4RangeHandler(ICityIPv4Repository cityIPv4Repository) => _cityIPv4Repository = cityIPv4Repository;

		public async Task<ResponseEntity<bool>> Handle(MultiCreateCityIPv4Range request, CancellationToken cancellationToken)
		{
			if (request.CityIPv4Ranges.Any())
			{
				IEnumerable<CityIPv4Range?> cityIPv4Ranges = request.CityIPv4Ranges
					.Select(x => CityIPv4Range.Create(x))
					.Where(x => x.IsSuccess && x.Entity != null)
					.Select(x => x.Entity);

				bool res = await _cityIPv4Repository.MultiInsertCityIPv4RangeAsync(cityIPv4Ranges, cancellationToken);
				return new ResponseEntity<bool>()
				{
					IsSuccess = true,
					Entity = res,
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
