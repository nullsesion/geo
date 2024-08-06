using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using MediatR;
using NpgsqlTypes;

namespace Geo.Application.CQRS.City.Commands.CreateCityIPv4Range
{
	public class CreateCityIPv4Range : IRequest<ResponseEntity<string>>, ICityIPv4Range
	{
		public string Network { get; set; }
		public int? GeonameId { get; set; }
		public int? RegisteredCountryGeoNameId { get; set; }
		public int? RepresentedCountryGeoNameId { get; set; }
		public bool IsAnonymousProxy { get; set; }
		public bool IsSatelliteProvider { get; set; }
		public bool? IsAnycast { get; set; }
		public NpgsqlPoint? Location { get; set; }
		public int? AccuracyRadius { get; set; }
	}
}
