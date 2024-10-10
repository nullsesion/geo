using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Geo.DataSeeding.Services
{
	public class Config: IConfiguration
	{
		private IConfiguration _config { get; set; }
		public Config()
		{
			IConfigurationBuilder builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false);

			_config = builder.Build();
		}

		public IConfigurationSection GetSection(string key) => _config.GetSection(key);

		public IEnumerable<IConfigurationSection> GetChildren() => _config.GetChildren();
		
		public IChangeToken GetReloadToken() => _config.GetReloadToken();
		
		public string? this[string key]
		{
			get => this[key];
			set => this[key] = value;
		}
	}
}
