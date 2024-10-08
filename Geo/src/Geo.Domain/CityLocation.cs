﻿using Geo.Domain.Shared.Contracts;
using Geo.DomainShared.Contracts;

namespace Geo.Domain
{
	public class CityLocation: ICityLocation
	{
		public int GeonameId { get; set; }

		public string LocaleCode { get; set; }

		public string ContinentCode { get; set; }

		public string ContinentName { get; set; }

		public string CountryIsoCode { get; set; }

		public string CountryName { get; set; }

		public string Subdivision1IsoCode { get; set; }
		
		public string Subdivision1Name { get; set; }

		public string Subdivision2IsoCode { get; set; }

		public string Subdivision2Name { get; set; }

		public string CityName { get; set; }

		public int? MetroCode { get; set; }

		public string TimeZone { get; set; }

		public bool IsInEuropeanUnion { get; set; } = false;

	}
}
