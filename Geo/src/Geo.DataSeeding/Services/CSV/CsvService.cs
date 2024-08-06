using CsvHelper;
using Geo.DataSeeding.Services.CSV.Models;
using Geo.DomainShared;
using Geo.DomainShared.Contracts;
using System.Globalization;
using MediatR;
using Geo.Application.CQRS.Country.Commands.CreateCountryRange;
using Geo.Application.CQRS.Country.Commands.MultiCreateCountryRange;
using Geo.Application.CQRS.Country.Commands.CreateCountryLocation;
using Geo.Application.CQRS.Country.Commands.TruncateCountryLocation;
using Geo.Application.CQRS.Country.Commands.TruncateTable;
using Geo.Application.CQRS.City.Commands.TruncateCityIPv4Range;
using Geo.Application.CQRS.City.Commands.CreateCityIPv4Range;
using Geo.Application.CQRS.City.Commands.MultiCreateCityIPv4Range;
using NpgsqlTypes;
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
			mediator.Send(new TruncateCountryLocation(), CancellationToken.None);
			IEnumerable<FileInfo> geoLite2CountryLocations = FindFile(fragmentName, path);
			foreach (FileInfo file in geoLite2CountryLocations)
			{
				using (var reader = new StreamReader(file.FullName))
				using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
				{
					IEnumerable<GeoLite2CountryLocations> records = csv.GetRecords<GeoLite2CountryLocations>();
					foreach (GeoLite2CountryLocations record in records)
					{
						CreateCountryLocation item = new CreateCountryLocation()
						{
							GeonameId = record.GeonameId,
							LocaleCode = record.LocaleCode,
							ContinentCode = record.ContinentCode,
							ContinentName = record.ContinentName,
							CountryIsoCode = record.CountryIsoCode,
							CountryName = record.CountryName,
							IsInEuropeanUnion = record.IsInEuropeanUnion,
						};
						ResponseEntity<int> res = mediator.Send(item, CancellationToken.None).Result;
						Console.Write("*");
					}
				}
			}
		}

		public void LoadGeoLite2CountryIPv4(string fragmentName, string path, IMediator mediator)
		{
			Console.WriteLine("LoadGeoLite2CountryIPv4");
			mediator.Send(new TruncateCountryIPv4(), CancellationToken.None);
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
						if (i % 4000 == 0) //2000
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
				}
			}
		}

		public void GeoLite2CityBlocksIPv4(string fragmentName, string path, IMediator mediator)
		{
			Console.WriteLine("GeoLite2CityBlocksIPv4");
			mediator.Send(new TruncateCityIPv4Range(), CancellationToken.None);
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
									: new NpgsqlPoint(geoLite2CityIPv4.Longitude ?? 0, geoLite2CityIPv4.Latitude ?? 0),
							AccuracyRadius = geoLite2CityIPv4.AccuracyRadius,
						});
						if (i % 4000 == 0)
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

				}
			}
		}
	}
/*
using (var context = new AppDbContext())
using (var reader = new StreamReader("zip\\GeoLite2-City-CSV_20240712\\GeoLite2-City-Blocks-IPv4.csv"))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
	int i = 0;
	IEnumerable<GeoLite2CityIPv4> records = csv.GetRecords<GeoLite2CityIPv4>();

	CityIPv4Entity t = mapper.Map<CityIPv4Entity>(records.First());
	Console.WriteLine("--------------------------------");
	Console.WriteLine("Network:" + t.Network);
	Console.WriteLine("IpMin: " + t.IpMin);
	Console.WriteLine("IpMax: " + t.IpMax);
	Console.WriteLine("GeonameId:" + t.GeonameId);
	Console.WriteLine("RegisteredCountryGeoNameId: " + t.RegisteredCountryGeoNameId);
	Console.WriteLine("RepresentedCountryGeoNameId: " + t.RepresentedCountryGeoNameId);
	Console.WriteLine("IsAnonymousProxy: " + t.IsAnonymousProxy);
	Console.WriteLine("IsSatelliteProvider:" + t.IsSatelliteProvider);
	Console.WriteLine("IsAnycast:" + t.IsAnycast);
	Console.WriteLine("Location: " + t.Location);
	Console.WriteLine("AccuracyRadius: " +t.AccuracyRadius);
	Console.WriteLine("--------------------------------");
	context.CityIPv4s.Add(t);
	context.SaveChanges();
}
*/
}
