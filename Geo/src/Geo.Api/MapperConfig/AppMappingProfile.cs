using AutoMapper;
using Geo.Api.Models;
using Geo.DomainShared.Contracts;
using Geo.Domain;

namespace Geo.Api.MapperConfig
{
	public class AppMappingProfile : Profile
	{
		public AppMappingProfile()
		{
			CreateMap<ICountryLocation, CountryInfo>();
			CreateMap<CountryIPv4Range, CountryResponse>();

			CreateMap<CityLocation, CityInfo>();
			CreateMap<CityIPv4Range, CityResponse>();
		}
	}
}
