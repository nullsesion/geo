using Geo.Application.Interfaces;
using Geo.Domain;
using MediatR;

namespace Geo.Application.IpLocations.Queries.Get
{
	public class GetIpLocation: IRequest<IpLocationVm>
	{
		public string Address { get; set; }
	}
}
