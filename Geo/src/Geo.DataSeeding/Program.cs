using Geo.DataSeeding;
using Geo.DataSeeding.Services;
using Geo.DataSeeding.Services.FileManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

ServiceProvider CreateServiceProvider()
{
	//IConfiguration config
	var collection = new ServiceCollection();
	collection.AddScoped<Execution>();
	collection.AddScoped<Display>();
	collection.AddScoped<DownloadManager>();
	
	//collection.AddDbContext<IGeoApiDbContext, GeoApiDbContext>(); ;
	//collection.AddScoped<IProductsRepository, ProductsRepository>();
	//collection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));
	return collection.BuildServiceProvider();
}


IConfigurationBuilder builder = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false);
IConfiguration config = builder.Build();


ServiceProvider serviceProvider = CreateServiceProvider(); //config
serviceProvider.GetRequiredService<Execution>().Run(config);
