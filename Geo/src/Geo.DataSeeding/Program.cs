using Geo.Application.CQRS.Country.Queries.GetCountry;
using Geo.Application.Interfaces;
using Geo.DataAccess;
using Geo.DataAccess.MapperConfig;
using Geo.DataAccess.Repositories;
using Geo.DataSeeding;
using Geo.DataSeeding.Interfaces;
using Geo.DataSeeding.Services;
using Geo.DataSeeding.Services.CSV;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


public class Program
{
	static async Task Main(string[] args)
	{
		IConfigurationBuilder builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false);

		IConfiguration config = builder.Build();

		ServiceProvider serviceProvider = CreateServiceProvider(config); //config
		await serviceProvider.GetRequiredService<Execution>().Run(config);
	}
	static ServiceProvider CreateServiceProvider(IConfiguration config)
	{
		var collection = new ServiceCollection();

		collection.AddDbContext<IGeoApiDbContext, GeoApiDbContext>(
			options => options.UseNpgsql(config.GetConnectionString(nameof(GeoApiDbContext)))
		);
		collection.AddSingleton<ICountryRepository, CountryRepository>();
		collection.AddSingleton<ICityIPv4Repository, CityIPv4Repository>();

		collection.AddSingleton<Execution>();
		collection.AddTransient<Display>();
		collection.AddSingleton<IStepPrepareDownload, DownloadManager>();
		collection.AddSingleton<IStepPrepareUnzip, UnzipFiles>();
		collection.AddSingleton<IInstall2Db, Install2Db>();
		collection.AddSingleton<IStepFinish, Seeding>();
		collection.AddSingleton<CsvService>();

		collection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCountry).Assembly));
		collection.AddAutoMapper(cfg =>
		{
			cfg.AddProfile(typeof(AppMappingProfile));
		});

		return collection.BuildServiceProvider();
	}
}
