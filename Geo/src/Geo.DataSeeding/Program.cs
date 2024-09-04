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
		collection.AddScoped<ICountryRepository, CountryRepository>();
		collection.AddScoped<ICityIPv4Repository, CityIPv4Repository>();

		collection.AddScoped<Execution>();
		collection.AddTransient<Display>();
		collection.AddScoped<IStepPrepareDownload, DownloadManager>();
		collection.AddScoped<IStepPrepareUnzip, UnzipFiles>();
		collection.AddScoped<IInstall2Db, Install2Db>();
		collection.AddScoped<IStepFinish, Seeding>();
		collection.AddScoped<CsvService>();

		collection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCountry).Assembly));
		collection.AddAutoMapper(cfg =>
		{
			cfg.AddProfile(typeof(AppMappingProfile));
		});

		return collection.BuildServiceProvider();
	}
}
