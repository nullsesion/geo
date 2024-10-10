using CSharpFunctionalExtensions;
using Geo.Domain;
using Geo.Domain.Shared.Contracts;
using MediatR;

namespace Geo.Application.CQRS.City.Commands.CreateCityIPv4Range
{
	public class CreateCityIPv4Range : IRequest<Result>, ICityIPv4Range
	{
		public string Network { get; set; }
		public int? GeonameId { get; set; }
		public int? RegisteredCountryGeoNameId { get; set; }
		public int? RepresentedCountryGeoNameId { get; set; }
		public bool IsAnonymousProxy { get; set; }
		public bool IsSatelliteProvider { get; set; }
		public bool? IsAnycast { get; set; }
		public Coordinate? Location { get; set; }
		public int? AccuracyRadius { get; set; }
	}
}
