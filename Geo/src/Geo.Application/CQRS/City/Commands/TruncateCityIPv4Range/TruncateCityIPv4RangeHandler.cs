using CSharpFunctionalExtensions;
using Geo.Application.Interfaces;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.City.Commands.TruncateCityIPv4Range
{
	public class TruncateCityIPv4RangeHandler: IRequestHandler<TruncateCityIPv4Range, Result>
	{
		private readonly ICityIPv4Repository _cityIPv4Repository;

		public TruncateCityIPv4RangeHandler(ICityIPv4Repository cityIPv4Repository) => _cityIPv4Repository = cityIPv4Repository;

		public async Task<Result> Handle(TruncateCityIPv4Range request, CancellationToken cancellationToken)
		{
			await _cityIPv4Repository.TruncateCityIPv4RangeAsync();
			await _cityIPv4Repository.SaveChangesAsync();
			return Result.Success();
		}
	}
}
