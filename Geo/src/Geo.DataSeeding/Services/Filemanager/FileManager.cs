using Spectre.Console;
using System.IO.Compression;
using System.IO;

namespace Geo.DataSeeding.Services.Filemanager
{
	public class FileManager
	{
		public async Task Run(IEnumerable<string> files)
		{

			var startLoad = new Rule("[green]Start load[/]");
			startLoad.Justification = Justify.Left;
			AnsiConsole.Write(startLoad);

			List<string> paths = new List<string>();
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

				if(fileLoder.FileExist())
					paths.Add(fileLoder.path);
			}
			var startUnzip = new Rule("[green]Unzip[/]");
			startUnzip.Justification = Justify.Left;
			AnsiConsole.Write(startUnzip);

			foreach (string path in paths)
			{
				AnsiConsole.MarkupLine($"[yellow]extract[/] path");
				Console.WriteLine(path);
				ZipFile.ExtractToDirectory(path,".");
			}

		}
	}
}
