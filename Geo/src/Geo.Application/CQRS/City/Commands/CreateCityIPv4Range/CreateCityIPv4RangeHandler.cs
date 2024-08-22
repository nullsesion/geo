using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.City.Commands.CreateCityIPv4Range
{
	public class CreateCityIPv4RangeHandler : IRequestHandler<CreateCityIPv4Range, ResponseEntity<string>>
	{
		private readonly ICityIPv4Repository _cityIPv4Repository;

		public CreateCityIPv4RangeHandler(ICityIPv4Repository cityIPv4Repository) => _cityIPv4Repository = cityIPv4Repository;

		public async Task<ResponseEntity<string>> Handle(CreateCityIPv4Range request, CancellationToken cancellationToken)
		{
			ResponseEntity<CityIPv4Range> cityIPv4Range = CityIPv4Range.Create(request);
			if (!cityIPv4Range.IsSuccess)
			{
				return new ResponseEntity<string>()
				{
					IsSuccess = cityIPv4Range.IsSuccess,
					ErrorDetail = cityIPv4Range.ErrorDetail,
				};
			}

			bool res = await _cityIPv4Repository
					.InsertCityIPv4RangeAsync(cityIPv4Range.Entity, CancellationToken.None)
				;

			await _cityIPv4Repository.SaveChangesAsync();

			return new ResponseEntity<string>()
			{
				IsSuccess = true,
				Entity = "",
			};
		}
	}
}
