using Geo.Application.CQRS.Country.CreateCountryRange;
using Geo.DataSeeding.Services.CSV;
using Geo.DataSeeding.Services.CSV.Models;
using Geo.DataSeeding.Services.FileManager;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata;
using Geo.DomainShared;

namespace Geo.DataSeeding
{
	public class Execution
	{
		private readonly DownloadManager _download;

		private readonly CsvService _csvHelper;

		private readonly IMediator _mediator;

		public Execution(IMediator mediator, DownloadManager download, CsvService csvHelper) => (_download, _csvHelper, _mediator) = (download, csvHelper, mediator);
		
		public async void Run(IConfiguration config)
		{
			//todo: change to string
			//_csvHelper.FindFile(new GeoLite2CountryIPv4());
			Console.WriteLine("------");

			CreateCountryIPv4Range createCountryIPv4Range = new CreateCountryIPv4Range()
			{
				Network = "1.0.1.0/24",
				IpMin = 0,
				IpMax = 0,
				GeonameId = 1814991,
				RegisteredCountryGeoNameId = 1814991,
				RepresentedCountryGeoNameId = null,
				IsAnonymousProxy = false,
				IsSatelliteProvider = false,
				IsAnycast = null,
			};
			ResponseEntity<string> res = _mediator.Send(createCountryIPv4Range, CancellationToken.None).Result;
			Console.WriteLine($"n: {res.Entity}");

		}

		private async Task DownloadFiles(IConfiguration config)
		{
			List<string> files = config.GetSection("urlsCsvLoad")
				.GetChildren()
				.ToList()
				.Where(x => x.Value != null)
				.Select(x => x.Value)
				.ToList()!;

			IEnumerable<string> fileExistList = await _download.Run(files.Select(x => new WebLoader(x)));
			var allFiles = _csvHelper.FindFile(new GeoLite2CountryLocations());
			foreach (var item in allFiles)
			{
				/*
				Console.WriteLine(item.GeonameId);
				Console.WriteLine(item.LocaleCode);
				Console.WriteLine(item.ContinentCode);
				Console.WriteLine(item.ContinentName);
				Console.WriteLine(item.CountryIsoCode);
				Console.WriteLine(item.CountryName);
				Console.WriteLine(item.IsInEuropeanUnion);
				*/

				break;
			}
		}
	}
}
