using AutoMapper;
using Geo.DataAccess.Entities;
using Geo.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Geo.DataAccess.MapperConfig
{
	public class AppMappingProfile: Profile
	{
		public AppMappingProfile()
		{
			CreateMap<CountryLocationEntity, CountryLocation>()
				.ForMember(dest => dest.ContinentName
					, opt => opt.MapFrom(src 
						=> (string)JObject.Parse(src.ContinentName)["en"]))
				.ForMember(dest => dest.CountryName
					, opt => opt.MapFrom(src 
						=> (string)JObject.Parse(src.ContinentName)["en"]));
		}
	}
}
