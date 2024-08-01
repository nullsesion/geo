using System.Net;

namespace Geo.DataSeeding.Services.FileManager
{
	public class WebLoader
	{
		private readonly string _url;
		public string Path { get; }

		public WebLoader(string url)
		{
			_url = url;
			Path = url.Split('/').Last();
		}

		public bool FileExist()
		{
			return File.Exists(Path);
		}

		public async Task<bool> DownloadFile()
		{
			/*
			byte[] data;
			using (var client = new HttpClient())
			using (HttpResponseMessage response = client.GetAsync(_url).Result)
			{
				if (response.IsSuccessStatusCode)
				{
					using (HttpContent content = response.Content)
					{
						data = content.ReadAsByteArrayAsync().Result;
						using (FileStream file = File.Create(Path))
							file.Write(data, 0, data.Length);
						return true;
					}
				}
			}
			*/
			byte[] data;
			using (var client = new HttpClient())
			using (HttpResponseMessage response = await client.GetAsync(_url))
			{
				if (response.IsSuccessStatusCode)
				{
					using (HttpContent content = response.Content)
					{
						data = await content.ReadAsByteArrayAsync();
						using (FileStream file = File.Create(Path))
							file.Write(data, 0, data.Length);
						return true;
					}
				}
			}
			return false;
		}
	}
}
