using Geo.Application.IpLocations.Commands.Create;

namespace Geo.Loader.CSV
{
	public delegate bool CsvLineValidateDelegate(string line);

	public delegate void CsvLinesLoad(IEnumerable<string> lines);

	public static class LoaderTools
	{
		private const int COUNT_LINE = 10;
		public static string? GetFileNameFromArgs(string[] args)
		{
			string? filename = args.FirstOrDefault(x => x.Contains(".csv"));
			return filename;
		}
		
		public static void ReadFile(FileInfo file, CsvLineValidateDelegate valid, CsvLinesLoad write)
		{
			StreamReader f = file.OpenText();
			int i = 0;
			List<string> lines = new List<string>();
			while (!f.EndOfStream)
			{
				string line = f.ReadLine() ?? "";
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
