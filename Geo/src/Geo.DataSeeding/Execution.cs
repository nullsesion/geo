using Geo.DataSeeding.Services.Filemanager;
using MediatR;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using System.IO;

namespace Geo.DataSeeding
{
	public class Execution
	{
		/*
		private readonly IMediator _mediator;
		public Execution(IMediator mediator) => _mediator = mediator;
		*/
		public Execution()
		{

		}
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

			FileManager fm = new FileManager();
			await fm.Run(files);
		}
	}
}
