using Geo.Domain;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.City.Queries
{
	public class GetCity : IRequest<ResponseEntity<CityIPv4Range>>
	{
		public string Ip { get; set; }
		public string LocaleCode { get; set; }
	}
}
