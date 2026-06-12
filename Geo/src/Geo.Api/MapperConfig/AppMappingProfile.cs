using Geo.Api.Models;
using Geo.Domain;
using Geo.DomainShared.Contracts;
using Mapster;

namespace Geo.Api.MapperConfig
{
	public class AppMappingProfile : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<ICountryLocation, CountryInfo>();
			config.NewConfig<CountryIPv4Range, CountryResponse>();
			config.NewConfig<CityLocation, CityInfo>();
			config.NewConfig<CityIPv4Range, CityResponse>();
		}
	}
}
