using Geo.Application.Common.Mappings;
using Geo.Domain.Models;

namespace Geo.Application.IpLocations.Queries.Get
{
    public class IpLocationVm: IMapWith<IpLocation>
	{
		public string Address { get; set; }
		public string Network { get; set; }
		public string CountryCode { get; set; }
		public string CountryName { get; set; }
	}
}
