using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.City.Commands.TruncateCityLocation
{
	public class TruncateCityLocation: IRequest<ResponseEntity<bool>>
	{
	}
}
