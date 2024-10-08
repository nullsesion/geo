using MediatR;
using CSharpFunctionalExtensions;
using Geo.Domain.Shared.Contracts;

namespace Geo.Application.CQRS.City.Commands.MultiCreateCityIPv4Range
{
	public class MultiCreateCityIPv4Range: IRequest<Result>
	{
		public required IEnumerable<ICityIPv4Range> CityIPv4Ranges { get; set; }
	}
}
