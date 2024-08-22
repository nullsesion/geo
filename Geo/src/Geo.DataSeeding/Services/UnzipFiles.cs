using Geo.DataSeeding.Interfaces;
using Microsoft.Extensions.Configuration;
using System.IO.Compression;

namespace Geo.DataSeeding.Services
{
	public class UnzipFiles: IStepPrepareUnzip
	{
		//private readonly IConfiguration _tmpDir;
		private readonly IStepPrepareDownload _stepPrepareDownload;
		private Display _display;

		public UnzipFiles(IStepPrepareDownload stepPrepareDownload, Display display) =>
			(_stepPrepareDownload, _display) = (stepPrepareDownload, display);

		public string RunStep(IConfiguration config)
		{
			List<string> list = _stepPrepareDownload.RunStep(config);

			string tmpDir = config["TmpDir"] ?? "_zip";

			if(Directory.Exists(tmpDir))
				Directory.Delete(tmpDir, true);

			Directory.CreateDirectory(tmpDir);

			foreach (string file in list)
			{
				if (File.Exists(file))
				{
					ZipFile.ExtractToDirectory(file, tmpDir);
					_display.Ok($"extract {file}");
				}
			}
			
			return tmpDir;
		}
	}
}
