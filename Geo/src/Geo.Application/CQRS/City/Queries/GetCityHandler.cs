﻿using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.DomainShared;
using MediatR;

namespace Geo.Application.CQRS.City.Queries
{
	public class GetCityHandler: IRequestHandler<GetCity, ResponseEntity<CityIPv4Range>>
	{
		private readonly ICityIPv4Repository _cityIPv4Repository;
		public GetCityHandler(ICityIPv4Repository cityIPv4Repository) 
			=> _cityIPv4Repository = cityIPv4Repository;

		public Task<ResponseEntity<CityIPv4Range>> Handle(GetCity request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
