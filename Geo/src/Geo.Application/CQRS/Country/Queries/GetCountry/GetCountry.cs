using Geo.Domain;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.Country.Queries.GetCountry
{
	public class GetCountry: IRequest<ResponseEntity<CountryIPv4Range>>
	{
		public string Ip { get; set; }
		public string LocaleCode { get; set; }
	}
}
