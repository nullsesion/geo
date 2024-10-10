using CSharpFunctionalExtensions;
using Geo.Domain;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.Country.Queries.GetCountry
{
	public class GetCountry: IRequest<Result<CountryIPv4Range>>
	{
		public string Ip { get; set; }
		public string LocaleCode { get; set; }
	}
}
