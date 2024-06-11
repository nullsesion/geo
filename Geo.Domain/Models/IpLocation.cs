using System;

namespace Geo.Domain.Models
{
    public class IpLocation
    {
		public const int MAX_LEN_ADDRESS = 50;
		public const int MAX_LEN_NETWORK = 50;
		public const int MAX_LEN_COUNTRYCODE = 10;
		
		public IpLocation(Guid id, string address, string network, uint ipMin, uint ipMax, string countryCode, string countryName)
		{
			Id = id;
			Address = address;
			Network = network;
			IpMin = ipMin;
			IpMax = ipMax;
			CountryCode = countryCode;
			CountryName = countryName;
		}

		public Guid Id { get; set; }
		public string Address { get; set; } = String.Empty;
		public string Network { get; set; } = String.Empty;
		public uint IpMin { get; set; } = 0;
		public uint IpMax { get; set; } = 0;
		public string CountryCode { get; set; } = String.Empty;
		public string CountryName { get; set; }	= String.Empty;
	}
}
