﻿using Geo.DomainShared.Contracts;

namespace Geo.DataAccess.Entities
{
	public class CountryLocationEntity : ICountryLocation
	{
		public int GeonameId { get; set; }
		//public string LocaleCode { get; set; }
		public string ContinentCode { get; set; }
		public string ContinentName { get; set; }
		public string CountryIsoCode { get; set; }
		public string CountryName { get; set; }
		public bool IsInEuropeanUnion { get; set; } = false;
	}
}
