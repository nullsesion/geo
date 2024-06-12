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
			bool tryIpToUint32 = request.Address.TryIpToUint32(out UInt32 int32Ip);
			if (!tryIpToUint32) return null;

			IpLocation? entity = await _dbContext.IpLocations
				.FirstOrDefaultAsync(x =>
					x.IpMin < int32Ip && x.IpMax > int32Ip
					, cancellationToken);
			
			if (entity == null) return null;
				
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
