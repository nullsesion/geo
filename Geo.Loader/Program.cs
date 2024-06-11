using Microsoft.Extensions.DependencyInjection;
using Geo.Application.IpLocations.Commands.Create;
using Geo.Application.Interfaces;
using Geo.Persistence;
using Geo.Loader.CSV;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Geo.Loader
{
	public class Program
	{
		static void Main(string[] args)
		{
			ServiceProvider CreateServiceProvider()
			{
				var collection = new ServiceCollection();
				collection.AddScoped<Execution>();

				IConfigurationRoot config = new ConfigurationBuilder()
					.AddJsonFile("appsettings.json")
					.AddEnvironmentVariables()//todo: use in future
					.Build();

				collection.AddDbContext<IGeoApiDbContext, GeoApiDbContext>(options =>
					{
						options.UseNpgsql(config.GetConnectionString(nameof(GeoApiDbContext)));
					}
				);

				collection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateIpLocation).Assembly));
				return collection.BuildServiceProvider();
			}

			string fileName = LoaderTools.GetFileNameFromArgs(args, "demogeoip.csv");
			if (LoaderTools.TryFindFile(fileName, out FileInfo? file))
			{
				ServiceProvider serviceProvider = CreateServiceProvider();
				serviceProvider.GetRequiredService<Execution>().Run(file);
			}
			else
			{
				Console.WriteLine("File Not Found");
			}


		}
	}
}
