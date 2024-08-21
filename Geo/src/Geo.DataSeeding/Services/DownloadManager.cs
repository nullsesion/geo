using Geo.DataSeeding.Interfaces;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Geo.DataSeeding.Services
{
	public class DownloadManager: IStepPrepareDownload
	{
		private Display _display;

		public DownloadManager(Display display) => _display = display;

		public void RunStep(IConfiguration config)
		{
			//get url from DB
			Dictionary<string, string> urls = GetUrlsFromConfig(config);
			List<Task> tasks = new List<Task> { };
			foreach (KeyValuePair<string, string> url in urls)
			{
				string fileLoder = GetFileNameByUrl(url.Value);
				if (File.Exists(fileLoder))
					if (!_display.Confirm($"file {fileLoder} already exist. Download again?", false))
						continue;

				Console.WriteLine(File.Exists(fileLoder));
				tasks.Add(DownloadFileAsynk(url.Value));
			}
			Task.WaitAll(tasks.ToArray());
		}
		/*
		private void DownloadFile(string url)
		{
			DownloadFileAsynk(url);
		}
		*/
		private Dictionary<string, string> GetUrlsFromConfig(IConfiguration config)
		{
			Dictionary<string, string> urls = config.GetSection("urlsCsvLoad")
				.GetChildren()
				.ToList()
				.Where(x => x.Value != null)
				.Select(x => new KeyValuePair<string, string>(x.Key, x.Value))
				.ToDictionary(x => x.Key, x => x.Value);

			return urls;
		}
		private string GetFileNameByUrl(string url) => url.Split('/').Last();

		private async Task DownloadFileAsynk(string url)
		{
			string path = GetFileNameByUrl(url);
			
			byte[] data;
			using (var client = new HttpClient())
			using (HttpResponseMessage response = await client.GetAsync(url))
			{
				if (response.IsSuccessStatusCode)
				{
					using (HttpContent content = response.Content)
					{
						data = await content.ReadAsByteArrayAsync();
						using (FileStream file = File.Create(path))
							file.Write(data, 0, data.Length);
					}
				}
			}
		}
	}
}
