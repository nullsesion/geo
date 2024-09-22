using Geo.DomainShared.Contracts;
using MediatR;
using Geo.DomainShared;
using CSharpFunctionalExtensions;

namespace Geo.Application.CQRS.City.Commands.MultiCreateCityIPv4Range
{
	public class MultiCreateCityIPv4Range: IRequest<Result>
	{
		public required IEnumerable<ICityIPv4Range> CityIPv4Ranges { get; set; }
	}
}
