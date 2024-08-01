using System.IO.Compression;

namespace Geo.DataSeeding.Services.FileManager
{
	public class DownloadManager
	{
		private readonly string _zip = "_zip";
		private readonly Display _display;
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
					var isSuccess = await fileLoder.DownloadFile();
					if (isSuccess)
						_display.Ok();
					else
						_display.Fail();
				}

				if (fileLoder.FileExist())
				{
					paths.Add(fileLoder.Path);

					if(Directory.Exists(_zip))
						Directory.Delete(_zip, true);

					ZipFile.ExtractToDirectory(fileLoder.Path, _zip);
					_display.Ok("unpack " + fileLoder.Path);
				}
			}
			return paths;
		}
	}
}
