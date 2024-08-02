using Geo.DataSeeding.Services.CSV;
using Geo.DataSeeding.Services.FileManager;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Geo.DataSeeding
{
	public class Execution
	{
		private readonly DownloadManager _download;

		private readonly CsvService _csvHelper;

		private readonly IMediator _mediator;

		public Execution(IMediator mediator, DownloadManager download, CsvService csvHelper) => (_download, _csvHelper, _mediator) = (download, csvHelper, mediator);
		
		public void Run(IConfiguration config) //async Task
		{
			/*
			List<string> files = config.GetSection("urlsCsvLoad")
				.GetChildren()
				.ToList()
				.Where(x => x.Value != null)
				.Select(x => x.Value)
				.ToList()!;

			IEnumerable<string> fileExistList = await _download.Run(files.Select(x => new WebLoader(x)));
			foreach (string file in fileExistList)
			{
				Console.WriteLine(file);
			}
			*/

			Dictionary<string, string> FileFragment = new Dictionary<string, string>()
			{
				{"GeoLite2CityIPv4"         ,"GeoLite2-City-Blocks-IPv4"},
				{"GeoLite2CountryIPv4"      ,"GeoLite2-Country-Blocks-IPv4"},
				{"GeoLite2CountryLocations" ,"GeoLite2-Country-Locations-en"}
			};

			foreach (KeyValuePair<string, string> csv in FileFragment)
			{
				switch (csv.Key)
				{
					
					case "GeoLite2CountryLocations":
						_csvHelper.LoadGeoLite2CountryLocations(csv.Value,_mediator);
						break;
					
					case "GeoLite2CountryIPv4":
						//_csvHelper.LoadGeoLite2CountryIPv4(csv.Value, _mediator);
						break;
					/*
					case "GeoLite2CityIPv4":
						//Console.WriteLine(csv.Value);
						_csvHelper.FindFile(csv.Value);
						foreach (FileInfo file in geoLite2CityIPv4)
						{
							Console.WriteLine(file.Name);
						}
						break;
					*/
				}
			}
		}

		private async Task<IEnumerable<FileInfo>> DownloadFiles(IConfiguration config, string fragmentName)
		{
			List<string> files = config.GetSection("urlsCsvLoad")
				.GetChildren()
				.ToList()
				.Where(x => x.Value != null)
				.Select(x => x.Value)
				.ToList()!;

			IEnumerable<string> fileExistList = await _download.Run(files.Select(x => new WebLoader(x)));
			IEnumerable<FileInfo> allFiles = _csvHelper.FindFile(fragmentName); //"GeoLite2-Country-Blocks-IPv4"
			
			return allFiles;
		}
	}
}
