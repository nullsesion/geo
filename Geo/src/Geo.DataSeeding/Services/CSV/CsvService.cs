using CsvHelper;
using Geo.Application.CQRS.Country.Commands.CreateCountryRange;
using Geo.DataSeeding.Services.CSV.Models;
using Geo.DomainShared;
using System.Globalization;
using MediatR;
using Geo.Application.CQRS.Country.Commands.MultiCreateCountryRange;
using Geo.DomainShared.Contracts;
using Spectre.Console;
using Geo.Application.CQRS.Country.Commands.CreateCountryLocation;
using System.Collections.Generic;

namespace Geo.DataSeeding.Services.CSV
{
	public class CsvService
	{
		public IEnumerable<FileInfo> FindFile(string _fragmentName)
		{
			DirectoryInfo dir = new DirectoryInfo("zip");
			IEnumerable<FileInfo> fileList = dir
				.GetFiles("*.csv", SearchOption.AllDirectories)
				.Where(x => x.Name.Contains(_fragmentName))
				;
			
			return fileList;
		}

		public void LoadGeoLite2CountryLocations(string fragmentName, IMediator mediator)
		{
			Console.WriteLine(fragmentName);
			IEnumerable<FileInfo> geoLite2CountryLocations = FindFile(fragmentName);
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
							ContinentCode = record.ContinentCode,
							ContinentName = record.ContinentName,
							CountryIsoCode = record.CountryIsoCode,
							CountryName = record.CountryName,
							IsInEuropeanUnion = record.IsInEuropeanUnion,
						};
						ResponseEntity<int> res = mediator.Send(item, CancellationToken.None).Result;
						Console.WriteLine("*");
					}
				}
			}
		}

		public void LoadGeoLite2CountryIPv4(string fragmentName, IMediator mediator)
		{
			DirectoryInfo dir = new DirectoryInfo("zip");
			IEnumerable<FileInfo> fileList = dir
					.GetFiles("*.csv", SearchOption.AllDirectories)
					.Where(x => x.Name.Contains(fragmentName))
				;

			foreach (FileInfo item in fileList)
			{
				//Console.WriteLine(item.FullName);
				using (var reader = new StreamReader(item.FullName))
				using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
				{
					int i = 0;
					IEnumerable<GeoLite2CountryIPv4> records = csv.GetRecords<GeoLite2CountryIPv4>();
					List<ICountryIPv4Range> buffer = new List<ICountryIPv4Range>();
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
						if (i % 2000 == 0)
						{
							MultiCreateCountryRangeIRequest list = new MultiCreateCountryRangeIRequest()
							{
								CountryIPv4Ranges = buffer,
							};
							ResponseEntity<IEnumerable<string>> res = mediator.Send(list, CancellationToken.None).Result;
							list.CountryIPv4Ranges = new List<ICountryIPv4Range>();
							buffer = new List<ICountryIPv4Range>();
							Console.WriteLine("*");
						}
					}
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
