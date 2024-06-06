using Geo.Application.IpLocations.Commands.Create;

namespace Geo.Loader.CSV
{
	public delegate bool CsvLineValidateDelegate(string line);

	public delegate void CsvLinesLoad(IEnumerable<string> lines);

	public static class LoaderTools
	{
		private const int COUNT_LINE = 10;
		public static string? GetFileNameFromArgs(string[] args, string defaultName)
		{
			if (defaultName == null) throw new ArgumentNullException(nameof(defaultName));
			string? filename = args.FirstOrDefault(x => x.Contains(".csv"));

			if (filename == null)
				return defaultName;

			return filename;
		}

		public static bool TryFindFile(string fileName, out FileInfo? file)
		{
			file = null;
			if (File.Exists(fileName))
			{
				file = new FileInfo(fileName);
				return true;
			}
			
			string currentDir = Directory.GetCurrentDirectory();
			DirectoryInfo? dir = new DirectoryInfo(currentDir);

			bool existFile = false;
			FileInfo[] files;
			do
			{
				files = dir.GetFiles();
				IEnumerable<string> hasFile = files
					.Select(x => x.Name)
					.Where(x => x == fileName);

				if (hasFile.Any())
					existFile = true;

				dir = dir.Parent;
				if (null == dir) return false;
			} while (!existFile);

			file = files.First(x => x.Name == fileName);

			return true;
		}

		public static void ReadFile(FileInfo file, CsvLineValidateDelegate valid, CsvLinesLoad write)
		{
			StreamReader f = file.OpenText();
			int i = 0;
			List<string> lines = new List<string>();
			while (!f.EndOfStream)
			{
				string line = f.ReadLine();
				if (valid(line))
				{
					lines.Add(line);
					if (i > COUNT_LINE)
					{
						write(lines);
						lines = new List<string>();
						i = 0;
					}
					i++;
				}
			}
			write(lines);
			f.Close();
		}

		public static CreateIpLocation ConvertStrToIPLocation(string str)
		{
			string[] res = str.Split(',');
			
			CreateIpLocation createIpLocationDTO = new CreateIpLocation
			{
				Id = Guid.NewGuid(),
				Address = res[0],
				Network = res[1],
				IpMin = UInt32.Parse(res[2]),
				IpMax = UInt32.Parse(res[3]),
				CountryCode = res[4],
				CountryName = res[5],
			};
			return createIpLocationDTO;
		}

	}
}
