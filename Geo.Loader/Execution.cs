using Geo.Application.IpLocations.Commands.Create;
using MediatR;
using System.Net;

namespace Geo.Loader
{
	public class Execution
	{
		private readonly IMediator _mediator;

		public Execution(IMediator mediator)
		{
			_mediator = mediator;
		}

		public void Run()
		{
			/*
			Task<Guid> сreateIpLocation = _mediator.Send(new CreateIpLocation
			{
				Id = Guid.NewGuid(),
				Address = "1.0.0.0",
				Network = "1.0.0.255",
				IpMin = 16777216,
				IpMax = 16777471,
				CountryCode = "AU",
				CountryName = "Australia",
			});
			сreateIpLocation.Wait();
			*/
			//download

		}
	}
}
