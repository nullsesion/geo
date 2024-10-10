using CSharpFunctionalExtensions;
using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using MediatR;

namespace Geo.Application.CQRS.City.Commands.MultiCreateCityIPv4Range
{
	public class MultiCreateCityIPv4RangeHandler: IRequestHandler<MultiCreateCityIPv4Range, Result>
	{
		private readonly ICityIPv4Repository _cityIPv4Repository;

		public MultiCreateCityIPv4RangeHandler(ICityIPv4Repository cityIPv4Repository) => _cityIPv4Repository = cityIPv4Repository;

		public async Task<Result> Handle(MultiCreateCityIPv4Range request, CancellationToken cancellationToken)
		{
			if (request.CityIPv4Ranges.Any())
			{
				IEnumerable<CityIPv4Range?> cityIPv4Ranges = request.CityIPv4Ranges
					.Select(x => CityIPv4Range.Create(x))
					.Where(x => x.IsSuccess && x.Value != null)
					.Select(x => x.Value);

				await _cityIPv4Repository.MultiInsertCityIPv4RangeAsync(cityIPv4Ranges, cancellationToken);
				return Result.Success();
			}
			return Result.Failure("Empty List");
		}
	}
}
