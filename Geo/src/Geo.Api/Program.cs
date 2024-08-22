using Geo.Application.CQRS.Country.Commands.CreateCountryRange;
using Geo.Application.Interfaces;
using Geo.DataAccess.Repositories;
using Geo.DataAccess;
using Geo.Api.Middlewares;
using Geo.Api.MapperConfig;
using Microsoft.EntityFrameworkCore;

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

			//ConfigurationManager t = builder.Configuration;

			builder.Services.AddDbContext<IGeoApiDbContext, GeoApiDbContext>(
				options => options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(GeoApiDbContext)))
			);

			builder.Services.AddScoped<ICountryRepository, CountryRepository>();
			builder.Services.AddScoped<ICityIPv4Repository, CityIPv4Repository>();

			builder.Services.AddAutoMapper(cfg =>
			{
				cfg.AddProfile(typeof(AppMappingProfile));
				cfg.AddProfile(typeof(Geo.DataAccess.MapperConfig.AppMappingProfile));
			});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			//builder.Services.Add

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseMiddleware<RemoveTrailingSpaces>();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
