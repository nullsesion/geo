using Geo.DataSeeding.Services.FileManager;
using Microsoft.Extensions.Configuration;

namespace Geo.DataSeeding
{
	public class Execution
	{
		private readonly DownloadManager _download;
		/*
		private readonly IMediator _mediator;
		public Execution(IMediator mediator) => _mediator = mediator;
		*/
		
		public Execution(DownloadManager download) => _download = download;

		public async void Run(IConfiguration config)
		{
			await DownloadFiles(config);
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
			foreach (string file in fileExistList)
			{
				Console.WriteLine(file);
			}
		}
	}
}
