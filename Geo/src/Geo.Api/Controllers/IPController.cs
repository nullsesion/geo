using Microsoft.AspNetCore.Mvc;
using System.Net;
using Geo.Application.CQRS.Country.Queries.GetCountry;
using Geo.Domain;
using Geo.DomainShared;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Geo.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class IPController : ControllerBase
	{
		private readonly ILogger<IPController> _logger;
		private readonly IMediator _mediator;

		
		public IPController(ILogger<IPController> logger, IMediator mediator)
		{
			(_logger, _mediator) = (logger, mediator);
		}

		[HttpGet(Name = "GetIP")]
		public async Task<IResult> Get([Required] string Ip, CancellationToken cancellationToken)
		{
			if (IPAddress.TryParse(Ip, out IPAddress? iPAddress))
			{
				ResponseEntity<CountryIPv4Range> response = await _mediator.Send(new GetCountry()
				{
					Ip = Ip
				}, cancellationToken);
				if (response.IsSuccess)
				{
					return Results.Json(response.Entity);
				}
				else
					return Results.BadRequest(response.ErrorDetail);
			}
			return Results.BadRequest("is not an IP address");
		}
		
	}
}
