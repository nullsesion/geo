using Geo.Domain;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using MediatR;
//using CreateCountryLocation = Geo.Application.CQRS.Country.Commands.CreateCountryLocation.CreateCountryLocation;

namespace Geo.Application.CQRS.Country.Commands.MultiCreateCountryLocation
{
	public class MultiCreateCountryLocation: IRequest<ResponseEntity<int>>
	{
		public required List<CountryLocation> CountryLocations { get; set; }
	}
}
