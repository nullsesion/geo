using System.IO.Compression;

namespace Geo.DataSeeding.Services.Filemanager
{
	public class WebLoader
	{
		private readonly string _url;
		public string path { get; }

		public WebLoader(string url)
		{
			_url = url;
			path = url.Split('/').Last();
		}

		public bool FileExist()
		{
			return File.Exists(path);
		}

		public async Task DownloadFile(Action action)
		{
			byte[] data;
			using (var client = new HttpClient())
			using (HttpResponseMessage response = await client.GetAsync(_url))
			using (HttpContent content = response.Content)
			{
				data = await content.ReadAsByteArrayAsync();
				using (FileStream file = File.Create(path))
					file.Write(data, 0, data.Length);
			}
			action();
		}
	}
}
