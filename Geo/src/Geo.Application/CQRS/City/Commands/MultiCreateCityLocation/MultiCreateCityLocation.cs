﻿using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using MediatR;


namespace Geo.Application.CQRS.City.Commands.MultiCreateCityLocation
{
	public class MultiCreateCityLocation : IRequest<ResponseEntity<IEnumerable<int>>>
	{
		public IEnumerable<ICityLocation> CityLocations { get; set; }
	}
}
