using Geo.DataSeeding.Services.CSV;
using Geo.DataSeeding.Services.CSV.Models;
using Geo.DataSeeding.Services.FileManager;
using Microsoft.Extensions.Configuration;

namespace Geo.DataSeeding
{
	public class Execution
	{
		private readonly DownloadManager _download;

		private readonly CsvService _csvHelper;
		/*
		private readonly IMediator _mediator;
		public Execution(IMediator mediator) => _mediator = mediator;
		*/
		
		public Execution(DownloadManager download, CsvService csvHelper) => (_download, _csvHelper) = (download, csvHelper);
		
		public async void Run(IConfiguration config)
		{
			//await DownloadFiles(config);
			_csvHelper.FindFile(new GeoLite2CountryIPv4());
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
				Console.WriteLine(item);
			}
		}
	}
}
