
using Geo.Application.CQRS.Country.Commands.CreateCountryRange;
using Geo.Application.Interfaces;
using Geo.DataAccess.Repositories;
using Geo.DataAccess;

namespace Geo.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCountryIPv4Range).Assembly));
			builder.Services.AddControllers();

			builder.Services.AddDbContext<IGeoApiDbContext, GeoApiDbContext>();

			builder.Services.AddScoped<ICountryRepository, CountryRepository>();

			builder.Services.AddScoped<ICityIPv4Repository, CityIPv4Repository>();
			builder.Services.AddScoped<ICityLocationRepository, CityLocationRepository>();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
