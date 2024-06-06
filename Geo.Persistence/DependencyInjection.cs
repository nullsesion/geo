
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Geo.Persistence
{
	public static class DependencyInjection
	{
		/*
		public static IServiceCollection AddPersistence(this IServiceCollection
			services, IConfiguration configuration)
		{
			var connectionString = configuration["DbConnection"];
			services.AddDbContext<GeoApiDbContext>( optionsBuilder =>
			{
				optionsBuilder.UseNpgsql(
					@"Host=localhost;Port=5432;Database=GeoLocation;User ID=postgresql;Password=postgresql");
			});
			services.AddScoped<GeoApiDbContext>(provider =>
				provider.GetService<GeoApiDbContext>());
			return services;
		}
		*/
	}
}
