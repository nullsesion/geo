using CSharpFunctionalExtensions;
using Geo.Domain.Shared.Contracts;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using MediatR;


namespace Geo.Application.CQRS.City.Commands.MultiCreateCityLocation
{
	public class MultiCreateCityLocation : IRequest<Result>
	{
		public IEnumerable<ICityLocation> CityLocations { get; set; }
	}
}
