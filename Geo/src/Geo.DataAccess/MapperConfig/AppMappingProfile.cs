using AutoMapper;
using CSharpFunctionalExtensions;
using Geo.DataAccess.Entities;
using Geo.Domain;
using Geo.Domain.Shared.Contracts;
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
						=> CoordinateToNpgsqlPoint(src.Location)
					));

			CreateMap<CityIPv4Entity, ICityIPv4Range>()
				.ForMember(dest => dest.Location
					, opt => opt.MapFrom(src
						=> NpgsqlPointToCoordinate(src.Location)
					));
		}

		public Coordinate NpgsqlPointToCoordinate(NpgsqlPoint? src)
		{
			Coordinate coordinate = null;
			Result<Coordinate> tryCoordinate = Coordinate.Create(src?.X ?? double.MinValue, src?.Y ?? double.MinValue);
			if (tryCoordinate.IsSuccess)
				coordinate = tryCoordinate.Value;
			return coordinate;
		}

		public NpgsqlPoint CoordinateToNpgsqlPoint(Coordinate coordinate)
		{
			if (coordinate is null)
				return new NpgsqlPoint(0, 0);

			NpgsqlPoint point = new NpgsqlPoint(coordinate.Latitude, coordinate.Longitude);
			return point;
		}
	}
}
