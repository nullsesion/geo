using CSharpFunctionalExtensions;
using MediatR;

namespace Geo.Application.CQRS.Country.Commands.TruncateCountryLocation
{
	public class TruncateCountryLocation: IRequest<Result>
	{
	}
}
