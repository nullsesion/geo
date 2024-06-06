using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Loader.Data
{
	public delegate void WriteDelegate(string line);
	public class CSVProvider
	{
		private string _fileName;
		private WriteDelegate _write;
		public CSVProvider(string fileName) => _fileName = fileName;

		private FileInfo OpenFile()
		{
			if (File.Exists(_fileName))
				return new FileInfo(_fileName);

			string currentDir = Directory.GetCurrentDirectory();
			DirectoryInfo? dir = new DirectoryInfo(currentDir);

			bool exitWhile = false;
			bool existFile = false;
			FileInfo[] files = dir.GetFiles();
			do
			{
				files = dir.GetFiles();
				IEnumerable<string> hasFile = files
					.Select(x => x.Name)
					.Where(x => x == _fileName);

				if (hasFile.Any())
					existFile = true;

				dir = dir.Parent;
				if (null == dir)
					exitWhile = true;

			} while (!(exitWhile || existFile));
			
			FileInfo fileCvs = files.First(x => x.Name == _fileName);
			return fileCvs;
		}
		public void RegisterWriteData(WriteDelegate write) => _write = write;
		public void Run()
		{
			FileInfo fileCvs = OpenFile();
			StreamReader f = fileCvs.OpenText();
			while (!f.EndOfStream)
			{
				string s = f.ReadLine();
				_write(s);
			}
			f.Close();

		}
	}
}
