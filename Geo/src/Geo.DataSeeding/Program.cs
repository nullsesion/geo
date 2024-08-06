using Geo.Application.CQRS.Country.Commands.CreateCountryRange;
using Geo.Application.Interfaces;
using Geo.DataAccess;
using Geo.DataAccess.Repositories;
using Geo.DataSeeding;
using Geo.DataSeeding.Services;
using Geo.DataSeeding.Services.CSV;
using Geo.DataSeeding.Services.FileManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

ServiceProvider CreateServiceProvider()
{
	//IConfiguration config
	var collection = new ServiceCollection();
	collection.AddDbContext<IGeoApiDbContext, GeoApiDbContext>();
	collection.AddScoped<ICountryIPv4Repository, CountryIPv4Repository>();
	collection.AddScoped<ICountryLocationRepository, CountryLocationRepository>();
	collection.AddScoped<ICityIPv4Repository, CityIPv4Repository>();
	// collection.AddScoped<Execution>();       collection.AddScoped<Display>();
	// collection.AddScoped<DownloadManager>(); collection.AddScoped<CsvService>();

	collection.AddScoped<Execution>();
	collection.AddScoped<Display>();
	collection.AddScoped<DownloadManager>();
	collection.AddScoped<CsvService>();
	
	collection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCountryIPv4Range).Assembly));
	return collection.BuildServiceProvider();
}


IConfigurationBuilder builder = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false);
IConfiguration config = builder.Build();

ServiceProvider serviceProvider = CreateServiceProvider(); //config
serviceProvider.GetRequiredService<Execution>().Run(config);
