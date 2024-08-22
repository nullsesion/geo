using Microsoft.AspNetCore.Mvc;
using System.Net;
using Geo.Application.CQRS.Country.Queries.GetCountry;
using Geo.Domain;
using Geo.DomainShared;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using AutoMapper;
using Geo.Api.Models;
using Geo.Application.CQRS.City.Queries;

namespace Geo.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class IPController : ControllerBase
	{
		private readonly ILogger<IPController> _logger;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public IPController(ILogger<IPController> logger, IMediator mediator, IMapper mapper)
		{
			(_logger, _mediator, _mapper) = (logger, mediator, mapper);
		}

		[HttpGet(Name = "GetIP")]
		public async Task<IResult> Get([Required] string Ip, CancellationToken cancellationToken)
		{
			ResponseEntity<CityIPv4Range> response = await _mediator.Send(new GetCity()
			{
				Ip = Ip,
				LocaleCode = "en"
			}, cancellationToken);

			if (response.IsSuccess)
			{
				return Results.Json(_mapper.Map<City>(response.Entity));
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
				return Results.Json(_mapper.Map<Country>(response.Entity) );
			}
			return Results.BadRequest(response.ErrorDetail);
			*/
			return Results.BadRequest(response.ErrorDetail);
		}
		
	}
}
