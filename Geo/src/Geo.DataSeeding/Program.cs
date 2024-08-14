using Geo.Application.CQRS.Country.Queries.GetCountry;
using Geo.Application.Interfaces;
using Geo.DataAccess;
using Geo.DataAccess.MapperConfig;
using Geo.DataAccess.Repositories;
using Geo.DataSeeding;
using Geo.DataSeeding.Services;
using Geo.DataSeeding.Services.CSV;
using Geo.DataSeeding.Services.FileManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

ServiceProvider CreateServiceProvider(IConfiguration config)
{
	//IConfiguration config
	var collection = new ServiceCollection();

	//builder.Build().GetConnectionString()

	collection.AddDbContext<IGeoApiDbContext, GeoApiDbContext>(
		options => options.UseNpgsql(config.GetConnectionString(nameof(GeoApiDbContext)))//"Host=localhost;Port=5432;Database=geo;User ID=postgres;Password=postgres"
	);
	collection.AddScoped<ICountryRepository, CountryRepository>();
	collection.AddScoped<ICityIPv4Repository, CityIPv4Repository>();

	collection.AddScoped<Execution>();
	collection.AddScoped<Display>();
	collection.AddScoped<DownloadManager>();
	collection.AddScoped<CsvService>();
	
	collection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCountry).Assembly));
	collection.AddAutoMapper(cfg =>
	{
		cfg.AddProfile(typeof(AppMappingProfile));
	});

	return collection.BuildServiceProvider();
}


IConfigurationBuilder builder = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false);

	IConfiguration config = builder.Build();

ServiceProvider serviceProvider = CreateServiceProvider(config); //config
serviceProvider.GetRequiredService<Execution>().Run(config);
