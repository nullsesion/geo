using MediatR;
using CsvHelper;
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
using CSharpFunctionalExtensions;


namespace Geo.DataSeeding.Services.CSV
{
	public class CsvService
	{
		private readonly Display _display;

		public CsvService(Display display) => _display = display;

		public IEnumerable<FileInfo> FindFile(string _fragmentName,string path)
		{
			DirectoryInfo dir = new DirectoryInfo(path);
			IEnumerable<FileInfo> fileList = dir
					.GetFiles("*.csv", SearchOption.AllDirectories)
					.Where(x => x.Name.Contains(_fragmentName))
				;

			return fileList;
		}
		
		public async Task LoadGeoLite2CountryLocations(string fragmentName, string path, IMediator mediator)
		{
			_display.WriteLine("LoadGeoLite2CountryLocations");

			IEnumerable <FileInfo> geoLite2CountryLocations = FindFile(fragmentName, path);
			foreach (FileInfo file in geoLite2CountryLocations)
			{
				using (var reader = new StreamReader(file.FullName))
				using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
				{
					//int i = 0;
					GeoLite2CountryLocations[] records = csv.GetRecords<GeoLite2CountryLocations>().ToArray();
					List<ICountryLocation> countryLocations = new List<ICountryLocation>();
					MultiCreateCountryLocation buffer = new MultiCreateCountryLocation()
					{
						CountryLocations = (new List<ICountryLocation>()) as IEnumerable<ICountryLocation>
					};
					int total = records.Count();
					for (int i = 0; i < total; i++)
					{
						GeoLite2CountryLocations record = records[i];
						countryLocations.Add(new CountryLocation()
						{
							GeonameId = record.GeonameId,
							LocaleCode = record.LocaleCode,
							ContinentCode = record.ContinentCode,
							ContinentName = record.ContinentName,
							CountryIsoCode = record.CountryIsoCode,
							CountryName = record.CountryName,
							IsInEuropeanUnion = record.IsInEuropeanUnion,
						});
						if ((i % 100 == 0 && i > 1) || (i + 1) == total )
						{
							buffer.CountryLocations = countryLocations;
							await mediator.Send(buffer, CancellationToken.None);
							buffer = new MultiCreateCountryLocation()
							{
								CountryLocations = (new List<ICountryLocation>()) as IEnumerable<ICountryLocation>
							};
							_display.Write("*");
						}
					}
					_display.WriteLine();
					_display.WriteLine("---------------------------------------");
				}
			}
		}

		public async Task LoadGeoLite2CountryIPv4(string fragmentName, string path, IMediator mediator)
		{
			_display.WriteLine("LoadGeoLite2CountryIPv4");
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
					Result res = new ();
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
						if (i % 4000 == 0)
						{
							list = new MultiCreateCountryRangeIRequest()
							{
								CountryIPv4Ranges = buffer,
							};
							res = await mediator.Send(list, CancellationToken.None);
							list.CountryIPv4Ranges = new List<ICountryIPv4Range>();
							buffer = new List<ICountryIPv4Range>();
							_display.Write("*");
						}
					}
					list = new MultiCreateCountryRangeIRequest()
					{
						CountryIPv4Ranges = buffer,
					};
					res = await mediator.Send(list, CancellationToken.None);
					list.CountryIPv4Ranges = new List<ICountryIPv4Range>();
					_display.Write("*");
					_display.WriteLine();
					_display.WriteLine("---------------------------------------");
				}
			}
		}
		public async Task GeoLite2CityBlocksIPv4(string fragmentName, string path, IMediator mediator)
		{
			_display.WriteLine("GeoLite2CityBlocksIPv4");
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
					Result res;
					foreach (GeoLite2CityIPv4 geoLite2CityIPv4 in records)
					{
						i++;
						Coordinate coordinate = null;
						Result<Coordinate> tryCoordinate = Coordinate.Create(geoLite2CityIPv4.Longitude ?? double.MinValue,
							geoLite2CityIPv4.Latitude ?? double.MinValue);
						if (tryCoordinate.IsSuccess)
							coordinate = tryCoordinate.Value;


						buffer.Add(new CreateCityIPv4Range()
						{
							Network = geoLite2CityIPv4.Network,
							GeonameId = geoLite2CityIPv4.GeonameId,
							RegisteredCountryGeoNameId = geoLite2CityIPv4.RegisteredCountryGeoNameId,
							RepresentedCountryGeoNameId = geoLite2CityIPv4.RepresentedCountryGeoNameId,
							IsAnonymousProxy = geoLite2CityIPv4.IsAnonymousProxy,
							IsSatelliteProvider = geoLite2CityIPv4.IsSatelliteProvider,
							IsAnycast = geoLite2CityIPv4.IsAnycast,
							Location = coordinate,
							AccuracyRadius = geoLite2CityIPv4.AccuracyRadius,
						});
						
						if (i % 4000 == 0)//4000
						{
							multiCreateCityIPv4Range = new MultiCreateCityIPv4Range()
							{
								CityIPv4Ranges = buffer
							};
							await mediator.Send(multiCreateCityIPv4Range, CancellationToken.None);
							buffer = new List<CreateCityIPv4Range>();
							_display.Write("*");
						}
					}
					multiCreateCityIPv4Range = new MultiCreateCityIPv4Range()
					{
						CityIPv4Ranges = buffer
					};
					await mediator.Send(multiCreateCityIPv4Range, CancellationToken.None);
					buffer = new List<CreateCityIPv4Range>();
					_display.Write("*");
					_display.WriteLine();
					_display.WriteLine("---------------------------------------");
				}
			}
		}
		public async Task GeoLite2CityLocations(string fragmentName, string path, IMediator mediator)
		{
			_display.WriteLine("GeoLite2CityLocations");
			DirectoryInfo dir = new DirectoryInfo(path);
			IEnumerable<FileInfo> fileList = dir
				.GetFiles("*.csv", SearchOption.AllDirectories)
				.Where(x => x.Name.Contains(fragmentName))
				;

			foreach (FileInfo item in fileList)
			{
				_display.WriteLine(item.Name);
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
							await mediator.Send(new MultiCreateCityLocation()
							{
								CityLocations = buffer,
							}, CancellationToken.None);
							buffer = new List<CreateCityLocation>();
							_display.Write("*");
						}
					}
					
					await mediator.Send(new MultiCreateCityLocation()
					{
						CityLocations = buffer,
					}, CancellationToken.None);

					_display.Write("*");
					_display.WriteLine();
					_display.WriteLine("---------------------------------------");
					await mediator.Send(new MultiCreateCityLocation()
					{
						CityLocations = buffer,
					}, CancellationToken.None);
				}
			}
		}
	}
}
