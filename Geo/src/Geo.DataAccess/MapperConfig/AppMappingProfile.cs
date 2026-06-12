using CSharpFunctionalExtensions;
using Geo.DataAccess.Entities;
using Geo.Domain;
using Geo.Domain.Shared.Contracts;
using Geo.DomainShared.Contracts;
using GeoLoad.Entities;
using Mapster;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NpgsqlTypes;

namespace Geo.DataAccess.MapperConfig
{
	public class AppMappingProfile : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<CountryLocationEntity, CountryLocation>()
				.Map(dest => dest.ContinentName, src => (string)JObject.Parse(src.ContinentName)["en"])
				.Map(dest => dest.CountryName, src => (string)JObject.Parse(src.ContinentName)["en"]);

			config.NewConfig<CountryLocation, CountryLocationEntity>()
				.Map(dest => dest.ContinentName
					, src => JsonConvert.SerializeObject(
						new Dictionary<string, string>() { { src.LocaleCode, src.ContinentName } }))
				.Map(dest => dest.CountryName
					, src => JsonConvert.SerializeObject(
						new Dictionary<string, string>() { { src.LocaleCode, src.CountryName } }));

			config.NewConfig<ICountryLocation, CountryLocationEntity>()
				.Map(dest => dest.ContinentName
					, src => JsonConvert.SerializeObject(
						new Dictionary<string, string>() { { src.LocaleCode, src.ContinentName } }))
				.Map(dest => dest.CountryName
					, src => JsonConvert.SerializeObject(
						new Dictionary<string, string>() { { src.LocaleCode, src.CountryName } }));

			config.NewConfig<CityLocation, CityLocationEntity>()
				.Map(dest => dest.Subdivision
					, src => JsonConvert.SerializeObject(new Dictionary<string, string>()
					{
						{ "Subdivision1IsoCode", src.Subdivision1IsoCode },
						{ "Subdivision1Name", src.Subdivision1Name },
						{ "Subdivision2IsoCode", src.Subdivision2IsoCode },
						{ "Subdivision2Name", src.Subdivision2Name },
					}))
				.Map(dest => dest.ContinentName
					, src => JsonConvert.SerializeObject(
						new Dictionary<string, string>() { { src.LocaleCode, src.ContinentName } }))
				.Map(dest => dest.CountryName
					, src => JsonConvert.SerializeObject(
						new Dictionary<string, string>() { { src.LocaleCode, src.CountryName } }))
				.Map(dest => dest.CityName
					, src => JsonConvert.SerializeObject(
						new Dictionary<string, string>() { { src.LocaleCode, src.CityName } }));

			config.NewConfig<ICityLocation, CityLocationEntity>()
				.Map(dest => dest.Subdivision
					, src => JsonConvert.SerializeObject(new Dictionary<string, string>()
					{
						{ "Subdivision1IsoCode", src.Subdivision1IsoCode },
						{ "Subdivision1Name", src.Subdivision1Name },
						{ "Subdivision2IsoCode", src.Subdivision2IsoCode },
						{ "Subdivision2Name", src.Subdivision2Name },
					}))
				.Map(dest => dest.ContinentName
					, src => JsonConvert.SerializeObject(
						new Dictionary<string, string>() { { src.LocaleCode, src.ContinentName } }))
				.Map(dest => dest.CountryName
					, src => JsonConvert.SerializeObject(
						new Dictionary<string, string>() { { src.LocaleCode, src.CountryName } }))
				.Map(dest => dest.CityName
					, src => JsonConvert.SerializeObject(
						new Dictionary<string, string>() { { src.LocaleCode, src.CityName } }));

			config.NewConfig<CityLocationEntity, CityLocation>()
				.Map(dest => dest.ContinentName
					, src => (string)JObject.Parse(src.ContinentName)["en"])
				.Map(dest => dest.CountryName
					, src => (string)JObject.Parse(src.ContinentName)["en"])
				.Map(dest => dest.CityName
					, src => (string)JObject.Parse(src.CityName)["en"]);

			config.NewConfig<CityIPv4Range, CityIPv4Entity>()
				.Map(dest => dest.Location
					, src => CoordinateToNpgsqlPoint(src.Location));

			config.NewConfig<CityIPv4Entity, ICityIPv4Range>()
				.Map(dest => dest.Location
					, src => NpgsqlPointToCoordinate(src.Location));
		}

		private static Coordinate NpgsqlPointToCoordinate(NpgsqlPoint? src)
		{
			Coordinate coordinate = null;
			Result<Coordinate> tryCoordinate = Coordinate.Create(src?.X ?? double.MinValue, src?.Y ?? double.MinValue);
			if (tryCoordinate.IsSuccess)
				coordinate = tryCoordinate.Value;
			return coordinate;
		}

		private static NpgsqlPoint CoordinateToNpgsqlPoint(Coordinate coordinate)
		{
			if (coordinate is null)
				return new NpgsqlPoint(0, 0);

			return new NpgsqlPoint(coordinate.Latitude, coordinate.Longitude);
		}
	}
}
