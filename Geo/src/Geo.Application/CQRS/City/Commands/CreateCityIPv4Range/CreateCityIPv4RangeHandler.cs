using CSharpFunctionalExtensions;
using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.City.Commands.CreateCityIPv4Range
{
	public class CreateCityIPv4RangeHandler : IRequestHandler<CreateCityIPv4Range, Result>
	{
		private readonly ICityIPv4Repository _cityIPv4Repository;

		public CreateCityIPv4RangeHandler(ICityIPv4Repository cityIPv4Repository) => _cityIPv4Repository = cityIPv4Repository;

		public async Task<Result> Handle(CreateCityIPv4Range request, CancellationToken cancellationToken)
		{
			var cityIPv4Range = CityIPv4Range.Create(request);
			if (cityIPv4Range.IsFailure)
			{
				return cityIPv4Range;
			}

			bool res = await _cityIPv4Repository
					.InsertCityIPv4RangeAsync(cityIPv4Range.Value, CancellationToken.None)
				;

			await _cityIPv4Repository.SaveChangesAsync();

			return cityIPv4Range;
		}
	}
}
