﻿using CSharpFunctionalExtensions;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using MediatR;

namespace Geo.Application.CQRS.Country.Commands.CreateCountryLocation
{
	public class CreateCountryLocation : IRequest<Result>, ICountryLocation
	{
		public int GeonameId { get; set; }
		public string LocaleCode { get; set; }
		public string ContinentCode { get; set; }
		public string ContinentName { get; set; }
		public string CountryIsoCode { get; set; }
		public string CountryName { get; set; }
		public bool IsInEuropeanUnion { get; set; }
	}
}
