using CSharpFunctionalExtensions;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using MediatR;

namespace Geo.Application.CQRS.Country.Commands.MultiCreateCountryRange
{
	public class MultiCreateCountryRangeIRequest : IRequest<Result>
	{
		public required IEnumerable<ICountryIPv4Range> CountryIPv4Ranges { get; set; }
	}
}
