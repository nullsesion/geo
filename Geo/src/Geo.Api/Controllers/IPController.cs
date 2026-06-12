using Microsoft.AspNetCore.Mvc;
using System.Net;
using Geo.Domain;
using Geo.DomainShared;
using MediatR;
using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using Geo.Api.Models;
using Geo.Application.CQRS.City.Queries;
using Mapster;

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
		public async Task<JsonResult> Get([Required] string Ip, CancellationToken cancellationToken)
		{
			Result<CityIPv4Range> response = await _mediator.Send(new GetCity()
			{
				Ip = Ip,
				LocaleCode = "en"
			}, cancellationToken);

			if (response.IsSuccess)
			{
				return new JsonResult(TypeAdapter.Adapt<CityResponse>(response.Value));
			}
			/*
			ResponseEntity<CountryIPv4Range> response = await _mediator.Send(new GetCountry()
			{
				Ip = Ip,
				LocaleCode = "en"
			}
			, cancellationToken);
			if (response.IsSuccess)
			{
				return Results.Json(_mapper.Map<CountryResponse>(response.Entity) );
			}
			return Results.BadRequest(response.ErrorDetail);
			*/
			
			return new JsonResult(BadRequest(response.Error));
		}
		
	}
}
