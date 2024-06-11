using Geo.Application.Common;
using Geo.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Geo.Application.Common.Exceptions;
using AutoMapper;
using Geo.Domain.Models;

namespace Geo.Application.IpLocations.Queries.Get
{
    public class GetIpLocationHandler: IRequestHandler<GetIpLocation, IpLocationVm>
	{
		private readonly IGeoApiDbContext _dbContext;
		private readonly IMapper _mapper;

		public GetIpLocationHandler(IGeoApiDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}
			
		public async Task<IpLocationVm> Handle(GetIpLocation request, CancellationToken cancellationToken)
		{
			IpLocation? entity = await _dbContext.IpLocations
				.FirstOrDefaultAsync(x =>
					x.IpMin < request.Address.IpToUint32()
					&& x.IpMax > request.Address.IpToUint32(), cancellationToken);

			if (entity == null)
			{
				throw new NotFoundException(nameof(IpLocation), request.Address);
			}

			// todo fix automapper
			//return _mapper.Map<IpLocationVm>(entity);
			return new IpLocationVm()
			{
				Address = entity.Address,
				Network = entity.Network,
				CountryCode = entity.CountryCode,
				CountryName = entity.CountryName,
			};
		}
	}
}
