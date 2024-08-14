using MediatR;
using CsvHelper;
using NpgsqlTypes;
using Geo.DomainShared;
using System.Globalization;
using Geo.DomainShared.Contracts;
using Geo.DataSeeding.Services.CSV.Models;
using Geo.Application.CQRS.Country.Commands.TruncateTable;
using Geo.Application.CQRS.City.Commands.CreateCityLocation;
using Geo.Application.CQRS.City.Commands.CreateCityIPv4Range;
using Geo.Application.CQRS.City.Commands.TruncateCityLocation;
using Geo.Application.CQRS.City.Commands.TruncateCityIPv4Range;
using Geo.Application.CQRS.City.Commands.MultiCreateCityLocation;
using Geo.Application.CQRS.City.Commands.MultiCreateCityIPv4Range;
using Geo.Application.CQRS.Country.Commands.CreateCountryRange;
using Geo.Application.CQRS.Country.Commands.MultiCreateCountryRange;
using Geo.Application.CQRS.Country.Commands.TruncateCountryLocation;
using Geo.Application.CQRS.Country.Commands.MultiCreateCountryLocation;
using Geo.Domain;


namespace Geo.DataSeeding.Services.CSV
{
	public class CsvService
	{
		public IEnumerable<FileInfo> FindFile(string _fragmentName,string path)
		{
			DirectoryInfo dir = new DirectoryInfo(path);
			IEnumerable<FileInfo> fileList = dir
					.GetFiles("*.csv", SearchOption.AllDirectories)
					.Where(x => x.Name.Contains(_fragmentName))
				;

			return fileList;
		}

		public void LoadGeoLite2CountryLocations(string fragmentName, string path, IMediator mediator)
		{
			Console.WriteLine("LoadGeoLite2CountryLocations");
			var response = mediator.Send(new TruncateCountryLocation(), CancellationToken.None).Result;
			ResponseEntity<int> res = new ResponseEntity<int>();

			IEnumerable <FileInfo> geoLite2CountryLocations = FindFile(fragmentName, path);
			foreach (FileInfo file in geoLite2CountryLocations)
			{
				using (var reader = new StreamReader(file.FullName))
				using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
				{
					int i = 0;
					IEnumerable<GeoLite2CountryLocations> records = csv.GetRecords<GeoLite2CountryLocations>();
					MultiCreateCountryLocation buffer = new MultiCreateCountryLocation()
					{
						CountryLocations = new List<CountryLocation>()
					};
					foreach (GeoLite2CountryLocations record in records)
					{
						i++;
						buffer.CountryLocations.Add(new CountryLocation()
						{
							GeonameId = record.GeonameId,
							LocaleCode = record.LocaleCode,
							ContinentCode = record.ContinentCode,
							ContinentName = record.ContinentName,
							CountryIsoCode = record.CountryIsoCode,
							CountryName = record.CountryName,
							IsInEuropeanUnion = record.IsInEuropeanUnion,
						});

						if (i % 100 == 0)
						{
							res = mediator.Send(buffer, CancellationToken.None).Result;
							buffer = new MultiCreateCountryLocation()
							{
								CountryLocations = new List<CountryLocation>()
							};
							Console.Write("*");
						}
					}
					res = mediator.Send(buffer, CancellationToken.None).Result;
					Console.Write("*");
					Console.WriteLine();
					Console.WriteLine("---------------------------------------");
				}
			}
		}

		public void LoadGeoLite2CountryIPv4(string fragmentName, string path, IMediator mediator)
		{
			Console.WriteLine("LoadGeoLite2CountryIPv4");
			var response = mediator.Send(new TruncateCountryIPv4(), CancellationToken.None).Result;
			DirectoryInfo dir = new DirectoryInfo(path);
			IEnumerable<FileInfo> fileList = dir
					.GetFiles("*.csv", SearchOption.AllDirectories)
					.Where(x => x.Name.Contains(fragmentName))
				;

			foreach (FileInfo item in fileList)
			{
				using (var reader = new StreamReader(item.FullName))
				using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
				{
					int i = 0;
					IEnumerable<GeoLite2CountryIPv4> records = csv.GetRecords<GeoLite2CountryIPv4>();
					List<ICountryIPv4Range> buffer = new List<ICountryIPv4Range>();
					MultiCreateCountryRangeIRequest list = new MultiCreateCountryRangeIRequest()
					{
						CountryIPv4Ranges = null,
					};
					ResponseEntity<IEnumerable<string>> res = new ResponseEntity<IEnumerable<string>>();
					foreach (GeoLite2CountryIPv4 r in records)
					{
						i++;
						CreateCountryIPv4Range createCountryIPv4Range = new CreateCountryIPv4Range()
						{
							Network = r.Network,
							GeonameId = r.GeonameId,
							RegisteredCountryGeoNameId = r.RegisteredCountryGeoNameId,
							RepresentedCountryGeoNameId = r.RepresentedCountryGeoNameId,
							IsAnonymousProxy = r.IsAnonymousProxy,
							IsSatelliteProvider = r.IsSatelliteProvider,
							IsAnycast = r.IsAnycast,
						};
						buffer.Add(createCountryIPv4Range);
						if (i % 5000 == 0) //4000
						{
							list = new MultiCreateCountryRangeIRequest()
							{
								CountryIPv4Ranges = buffer,
							};
							res = mediator.Send(list, CancellationToken.None).Result;
							list.CountryIPv4Ranges = new List<ICountryIPv4Range>();
							buffer = new List<ICountryIPv4Range>();
							Console.Write("*");
						}
					}
					list = new MultiCreateCountryRangeIRequest()
					{
						CountryIPv4Ranges = buffer,
					};
					res = mediator.Send(list, CancellationToken.None).Result;
					list.CountryIPv4Ranges = new List<ICountryIPv4Range>();
					buffer = new List<ICountryIPv4Range>();
					Console.Write("*");
					Console.WriteLine();
					Console.WriteLine("---------------------------------------");
				}
			}
		}
		public void GeoLite2CityBlocksIPv4(string fragmentName, string path, IMediator mediator)
		{
			Console.WriteLine("GeoLite2CityBlocksIPv4");
			var response = mediator.Send(new TruncateCityIPv4Range(), CancellationToken.None).Result;
			DirectoryInfo dir = new DirectoryInfo(path);
			IEnumerable<FileInfo> fileList = dir
				.GetFiles("*.csv", SearchOption.AllDirectories)
				.Where(x => x.Name.Contains(fragmentName))
				;

			foreach (FileInfo item in fileList)
			{
				using (var reader = new StreamReader(item.FullName))
				using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
				{
					int i = 0;
					IEnumerable<GeoLite2CityIPv4> records = csv.GetRecords<GeoLite2CityIPv4>();
					List<CreateCityIPv4Range> buffer = new List<CreateCityIPv4Range>();
					MultiCreateCityIPv4Range multiCreateCityIPv4Range = new MultiCreateCityIPv4Range
					{
						CityIPv4Ranges = null
					};
					MultiCreateCountryRangeIRequest list = new MultiCreateCountryRangeIRequest()
					{
						CountryIPv4Ranges = null,
					};
					ResponseEntity<IEnumerable<string>> res;
					foreach (GeoLite2CityIPv4 geoLite2CityIPv4 in records)
					{
						i++;
						buffer.Add(new CreateCityIPv4Range()
						{
							Network = geoLite2CityIPv4.Network,
							GeonameId = geoLite2CityIPv4.GeonameId,
							RegisteredCountryGeoNameId = geoLite2CityIPv4.RegisteredCountryGeoNameId,
							RepresentedCountryGeoNameId = geoLite2CityIPv4.RepresentedCountryGeoNameId,
							IsAnonymousProxy = geoLite2CityIPv4.IsAnonymousProxy,
							IsSatelliteProvider = geoLite2CityIPv4.IsSatelliteProvider,
							IsAnycast = geoLite2CityIPv4.IsAnycast,
							Location =
								geoLite2CityIPv4.Longitude == null || geoLite2CityIPv4.Latitude == null
									? null
									: new Coordinate(geoLite2CityIPv4.Longitude ?? 0, geoLite2CityIPv4.Latitude ?? 0),
							AccuracyRadius = geoLite2CityIPv4.AccuracyRadius,
						});
						if (i % 4000 == 0)//4000
						{
							multiCreateCityIPv4Range = new MultiCreateCityIPv4Range()
							{
								CityIPv4Ranges = buffer
							};
							res = mediator.Send(multiCreateCityIPv4Range, CancellationToken.None).Result;
							buffer = new List<CreateCityIPv4Range>();
							Console.Write("*");
						}
					}
					multiCreateCityIPv4Range = new MultiCreateCityIPv4Range()
					{
						CityIPv4Ranges = buffer
					};
					res = mediator.Send(multiCreateCityIPv4Range, CancellationToken.None).Result;
					buffer = new List<CreateCityIPv4Range>();
					Console.Write("*");
					Console.WriteLine();
					Console.WriteLine("---------------------------------------");
				}
			}
		}
		public void GeoLite2CityLocations(string fragmentName, string path, IMediator mediator)
		{
			Console.WriteLine("GeoLite2CityLocations");
			var response = mediator.Send(new TruncateCityLocation(), CancellationToken.None).Result;
			DirectoryInfo dir = new DirectoryInfo(path);
			IEnumerable<FileInfo> fileList = dir
				.GetFiles("*.csv", SearchOption.AllDirectories)
				.Where(x => x.Name.Contains(fragmentName))
				;

			foreach (FileInfo item in fileList)
			{
				Console.WriteLine(item.Name);
				using (var reader = new StreamReader(item.FullName))
				using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
				{
					int i = 0;
					IEnumerable<GeoLite2CityLocations> records = csv.GetRecords<GeoLite2CityLocations>();
					List<CreateCityLocation> buffer = new List<CreateCityLocation>();
					foreach (GeoLite2CityLocations record in records)
					{
						i++;
						CreateCityLocation cityLocation = new CreateCityLocation()
						{
							GeonameId = record.GeonameId,
							LocaleCode = record.LocaleCode,
							ContinentCode = record.ContinentCode,
							ContinentName = record.ContinentName,
							CountryIsoCode = record.CountryIsoCode,
							CountryName = record.CountryName,
							Subdivision1IsoCode = record.Subdivision1IsoCode,
							Subdivision1Name = record.Subdivision1Name,
							Subdivision2IsoCode = record.Subdivision2IsoCode,
							Subdivision2Name = record.Subdivision2Name,
							CityName = record.CityName,
							MetroCode = record.MetroCode,
							TimeZone = record.TimeZone, 
							IsInEuropeanUnion = record.IsInEuropeanUnion,
						};

						buffer.Add(cityLocation);
						if (i % 4000 == 0)//4000
						{
							ResponseEntity<IEnumerable<int>> t = mediator.Send(new MultiCreateCityLocation()
							{
								CityLocations = buffer,
							}, CancellationToken.None).Result;
							buffer = new List<CreateCityLocation>();
							Console.Write("*");
						}
					}
					ResponseEntity<IEnumerable<int>> t2 = mediator.Send(new MultiCreateCityLocation()
					{
						CityLocations = buffer,
					}, CancellationToken.None).Result;
					buffer = new List<CreateCityLocation>();
					Console.Write("*");
					Console.WriteLine();
					Console.WriteLine("---------------------------------------");
					mediator.Send(new MultiCreateCityLocation()
					{
						CityLocations = buffer,
					}, CancellationToken.None);
				}
			}
		}
	}
}
