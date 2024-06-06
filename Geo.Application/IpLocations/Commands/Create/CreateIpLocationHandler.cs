using Geo.Application.Interfaces;
using Geo.Domain;
using MediatR;

namespace Geo.Application.IpLocations.Commands.Create
{
	public class CreateIpLocationHandler: IRequestHandler<CreateIpLocation, Guid>
	{
		private IGeoApiDbContext _dbContext;
		public CreateIpLocationHandler(IGeoApiDbContext dbContext) => _dbContext = dbContext;

		public async Task<Guid> Handle(CreateIpLocation request, CancellationToken cancellationToken)
		{
			IpLocation ipLocation = new IpLocation
			{
				Id = request.Id,
				Address = request.Address,
				Network = request.Network,
				IpMin = request.IpMin,
				IpMax = request.IpMax,
				CountryCode = request.CountryCode,
				CountryName = request.CountryName,
			};

			await _dbContext.IpLocations.AddAsync(ipLocation, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return ipLocation.Id;
		}
	}
}
