using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using MediatR;

namespace Geo.Application.CQRS.Country.Commands.MultiCreateCountryLocation
{
	public class MultiCreateCountryLocation: IRequest<ResponseEntity<bool>>
	{
		public required IEnumerable<ICountryLocation> CountryLocations { get; set; }
	}
}
