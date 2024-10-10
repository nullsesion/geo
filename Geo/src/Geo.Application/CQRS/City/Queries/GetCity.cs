using CSharpFunctionalExtensions;
using Geo.Domain;
using MediatR;

namespace Geo.Application.CQRS.City.Queries
{
	public class GetCity : IRequest<Result<CityIPv4Range>>
	{
		public string Ip { get; set; }
		public string LocaleCode { get; set; }
	}
}
