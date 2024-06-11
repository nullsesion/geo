using Geo.Application.Interfaces;
using Geo.Domain.Models;
using MediatR;
using System.Net;

namespace Geo.Application.IpLocations.Commands.Create
{
    public class CreateIpLocationHandler: IRequestHandler<CreateIpLocation, Guid>
	{
		private IGeoApiDbContext _dbContext;
		public CreateIpLocationHandler(IGeoApiDbContext dbContext) => _dbContext = dbContext;

		public async Task<Guid> Handle(CreateIpLocation request, CancellationToken cancellationToken)
		{
			IpLocation ipLocation = new IpLocation(request.Id,
				request.Address,
				request.Network,
				request.IpMin,
				request.IpMax,
				request.CountryCode,
				request.CountryName)
			;

			await _dbContext.IpLocations.AddAsync(ipLocation, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return ipLocation.Id;
		}
	}
}
