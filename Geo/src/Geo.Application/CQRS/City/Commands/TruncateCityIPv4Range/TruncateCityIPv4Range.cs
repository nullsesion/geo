using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.City.Commands.TruncateCityIPv4Range
{
	public class TruncateCityIPv4Range: IRequest<ResponseEntity<bool>>
	{
	}
}
