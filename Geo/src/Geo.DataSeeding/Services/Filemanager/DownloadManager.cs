using Spectre.Console;

namespace Geo.DataSeeding.Services.FileManager
{
	public class DownloadManager
	{
		private Display _display;
		public DownloadManager(Display display) => _display = display;

		public async Task<IEnumerable<string>> Run(IEnumerable<WebLoader> files)
		{
			_display.ShowHead("Start load");
			List<string> paths = new List<string>();
			foreach (var fileLoder in files)
			{
				bool load = fileLoder.FileExist();
				if (load)
					load = _display.Confirm($"file {fileLoder.Path} already exist. Download again?", false);
				else
					load = true;

				if (load)
				{
					var isSuccess = fileLoder.DownloadFile();
					if (isSuccess)
						_display.Ok();
					else
						_display.Fail();
					/*
					try
					{

					} 
					catch (Exception e)	{ throw; }
					*/
				}
					

				if(fileLoder.FileExist())
					paths.Add(fileLoder.Path);

				//////////////////////////////////

			}
			return paths;
			/*
			var startUnzip = new Rule("[green]Unzip[/]");
			startUnzip.Justification = Justify.Left;
			AnsiConsole.Write(startUnzip);

			foreach (string _path in paths)
			{
				AnsiConsole.MarkupLine($"[yellow]extract[/] _path");
				Console.WriteLine(_path);
				ZipFile.ExtractToDirectory(_path,".");
			}
			*/
		}
	}
}
