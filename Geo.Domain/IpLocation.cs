using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Domain
{
	public class IpLocation
	{
		public Guid Id { get; set; }

		public string Address { get; set; }
		public string Network { get; set; }
		public UInt32 IpMin { get; set; }
		public UInt32 IpMax { get; set; }
		public string CountryCode { get; set; }
		public string CountryName { get; set; }
	}
}
