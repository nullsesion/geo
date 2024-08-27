using AutoMapper;
using Geo.DataAccess.Entities;
using Geo.Domain;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using GeoLoad.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NpgsqlTypes;

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

			CreateMap<CountryIPv4Range, CountryIPv4Entity>();
			CreateMap<ICountryIPv4Range, CountryIPv4Entity>();
			

			CreateMap<CountryLocation, CountryLocationEntity>()
				.ForMember(dest => dest.ContinentName
					, opt => opt.MapFrom(src
						=> JsonConvert.SerializeObject(
							new Dictionary<string, string>() { { src.LocaleCode, src.ContinentName } }
						)))
				.ForMember(dest => dest.CountryName
					, opt => opt.MapFrom(src
						=> JsonConvert.SerializeObject(
							new Dictionary<string, string>() { { src.LocaleCode, src.CountryName } }
						)))
				;

			CreateMap<ICountryLocation, CountryLocationEntity>()
				.ForMember(dest => dest.ContinentName
					, opt => opt.MapFrom(src
						=> JsonConvert.SerializeObject(
							new Dictionary<string, string>() { { src.LocaleCode, src.ContinentName } }
						)))
				.ForMember(dest => dest.CountryName
					, opt => opt.MapFrom(src
						=> JsonConvert.SerializeObject(
							new Dictionary<string, string>() { { src.LocaleCode, src.CountryName } }
						)))
				;

			CreateMap<CityLocation, CityLocationEntity>()
				.ForMember(dest => dest.Subdivision
					, opt => opt.MapFrom(src
						=> JsonConvert.SerializeObject(new Dictionary<string, string>()
							{
								{ "Subdivision1IsoCode", src.Subdivision1IsoCode },
								{ "Subdivision1Name",    src.Subdivision1Name },
								{ "Subdivision2IsoCode", src.Subdivision2IsoCode },
								{ "Subdivision2Name",    src.Subdivision2Name },
							}
						)))
				.ForMember(dest => dest.ContinentName
					, opt => opt.MapFrom(src
						=> JsonConvert.SerializeObject(
							new Dictionary<string, string>() { { src.LocaleCode, src.ContinentName } }
						)))
				.ForMember(dest => dest.CountryName
					, opt => opt.MapFrom(src
						=> JsonConvert.SerializeObject(
							new Dictionary<string, string>() { { src.LocaleCode, src.CountryName } }
						)))
				.ForMember(dest => dest.CityName
					, opt => opt.MapFrom(src
						=> JsonConvert.SerializeObject(
							new Dictionary<string, string>() { { src.LocaleCode, src.CityName } }
						)))
				;
			CreateMap<ICityLocation, CityLocationEntity>()
				.ForMember(dest => dest.Subdivision
					, opt => opt.MapFrom(src
						=> JsonConvert.SerializeObject(new Dictionary<string, string>()
							{
								{ "Subdivision1IsoCode", src.Subdivision1IsoCode },
								{ "Subdivision1Name",    src.Subdivision1Name },
								{ "Subdivision2IsoCode", src.Subdivision2IsoCode },
								{ "Subdivision2Name",    src.Subdivision2Name },
							}
						)))
				.ForMember(dest => dest.ContinentName
					, opt => opt.MapFrom(src
						=> JsonConvert.SerializeObject(
							new Dictionary<string, string>() { { src.LocaleCode, src.ContinentName } }
						)))
				.ForMember(dest => dest.CountryName
					, opt => opt.MapFrom(src
						=> JsonConvert.SerializeObject(
							new Dictionary<string, string>() { { src.LocaleCode, src.CountryName } }
						)))
				.ForMember(dest => dest.CityName
					, opt => opt.MapFrom(src
						=> JsonConvert.SerializeObject(
							new Dictionary<string, string>() { { src.LocaleCode, src.CityName } }
						)))
				;

			CreateMap<CityLocationEntity,CityLocation>()
				.ForMember(dest => dest.ContinentName
					, opt => opt.MapFrom(src
						=> (string)JObject.Parse(src.ContinentName)["en"]))
				.ForMember(dest => dest.CountryName
					, opt => opt.MapFrom(src
						=> (string)JObject.Parse(src.ContinentName)["en"]))
				.ForMember(dest => dest.CityName
				, opt => opt.MapFrom(src
					=> (string)JObject.Parse(src.CityName)["en"]))
				;

			CreateMap<CityIPv4Range, CityIPv4Entity>()
				.ForMember(dest => dest.Location
					, opt => opt.MapFrom(src
						=> new NpgsqlPoint(src.Location.Longitude, src.Location.Latilude)
					));

			CreateMap<ICityIPv4Range, CityIPv4Entity>()
				.ForMember(dest => dest.Location
					, opt => opt.MapFrom(src
						=> new NpgsqlPoint(src.Location.Longitude, src.Location.Latilude)
					));

			CreateMap<CityIPv4Entity, ICityIPv4Range>()
				.ForMember(dest => dest.Location
					, opt => opt.MapFrom(src
						=> new Coordinate(src.Location.Value.X, src.Location.Value.Y)
					));
		}
	}
}
