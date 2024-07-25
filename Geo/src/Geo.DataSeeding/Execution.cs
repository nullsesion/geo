using Geo.DataSeeding.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

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
			/*
			//config
			List<string> files = config.GetSection("urlsCsvLoad")
				.GetChildren()
				.ToList()
				.Where(x => x.Value != null)
				.Select(x => x.Value)
				.ToList()!;

			var startLoad = new Rule("[green]Start load[/]");
			startLoad.Justification = Justify.Left;
			AnsiConsole.Write(startLoad);

			foreach (string file in files)
			{
				WebLoader fileLoder = new WebLoader(file);

				bool load = fileLoder.FileExist();
				if (load)
					load = AnsiConsole.Confirm($"file {fileLoder.path} already exist. Download again?", false);
				else
					load = true;

				if (load)
					await fileLoder.DownloadFile(() => AnsiConsole.MarkupLine("[green]Ok[/]"));
			}

			var startUnzip = new Rule("[green]Unzip[/]");
			startUnzip.Justification = Justify.Left;
			AnsiConsole.Write(startUnzip);

			WebLoader.ClearBeforeUnzip();

			foreach (string file in files)
			{
				WebLoader fileLoader = new WebLoader(file);
				await fileLoader.unzipFile(() => AnsiConsole.MarkupLine("[green]Ok[/]"));
			}
			*/
		}
	}
}
