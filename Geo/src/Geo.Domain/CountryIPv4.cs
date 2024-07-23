using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Domain
{
	public class CountryIPv4
	{
		public string Network { get; set; }
		public uint IpMin { get; set; }
		public uint IpMax { get; set; }
		public int? GeonameId { get; set; }
		public int? RegisteredCountryGeoNameId { get; set; }
		public int? RepresentedCountryGeoNameId { get; set; }
		public bool IsAnonymousProxy { get; set; }
		public bool IsSatelliteProvider { get; set; }
		public bool? IsAnycast { get; set; }
	}
}
