using Geo.Loader.Data;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Geo.Application;
using Geo.Application.IpLocations.Commands.Create;
using Geo.Loader;
using Geo.Application.Interfaces;
using Geo.Persistence;

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
				collection.AddScoped<IGeoApiDbContext, GeoApiDbContext>();

				collection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateIpLocation).Assembly));
				return collection.BuildServiceProvider();
			}
			//run 

			ServiceProvider serviceProvider = CreateServiceProvider();
			serviceProvider.GetRequiredService<Execution>().Run();
		}
	}
}
