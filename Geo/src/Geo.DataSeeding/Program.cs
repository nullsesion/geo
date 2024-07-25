using Geo.Application.Interfaces;
using Geo.DataAccess;
using Geo.DataSeeding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

ServiceProvider CreateServiceProvider(IConfiguration config)
{
	var collection = new ServiceCollection();
	collection.AddScoped<Execution>();
	collection.AddDbContext<IGeoApiDbContext, GeoApiDbContext>(); ;
	//collection.AddScoped<IProductsRepository, ProductsRepository>();
	//collection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));
	return collection.BuildServiceProvider();
}


IConfigurationBuilder builder = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false);
IConfiguration config = builder.Build();


ServiceProvider serviceProvider = CreateServiceProvider(config);
serviceProvider.GetRequiredService<Execution>().Run(config);
