using Geo.DataSeeding.Services.CSV.Models;
using System.Linq;

namespace Geo.DataSeeding.Services.CSV
{
	public class CsvService
	{
		public void Load(ICsvModel model)
		{

		}

		public IEnumerable<string> FindFile(ICsvModel model)
		{
			DirectoryInfo dir = new DirectoryInfo("zip");
			IEnumerable<FileInfo> fileList = dir
				.GetFiles("*.csv", SearchOption.AllDirectories)
				.Where(x => x.Name.Contains(model.GetPartFileName()))
				;
			foreach (FileInfo file in fileList)
			{
				Console.WriteLine(file.Name);
			}
			/*
			string[] allFiles = Directory.GetFiles(".");
			return allFiles;
			*/
			return new List<string>();
		}
	}
}
