using CSharpFunctionalExtensions;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using MediatR;

namespace Geo.Application.CQRS.Country.Commands.MultiCreateCountryLocation
{
	public class MultiCreateCountryLocation: IRequest<Result>
	{
		public required IEnumerable<ICountryLocation> CountryLocations { get; set; }
	}
}
