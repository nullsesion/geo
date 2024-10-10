using Geo.Application.Interfaces;
using Geo.DataAccess.Configuration;
using Geo.DataAccess.Entities;
using GeoLoad.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Geo.DataAccess
{
	public class GeoApiDbContext : DbContext, IGeoApiDbContext
	{
		private IConfiguration _configuration;

		/*
		public GeoApiDbContext(DbContextOptions options) : base(options)
		{
		}
		*/
		public GeoApiDbContext(IConfiguration configuration) => _configuration = configuration;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder
				.UseNpgsql(_configuration.GetConnectionString("GeoApiDbContext"))
				.UseSnakeCaseNamingConvention()
				//.LogTo(Console.WriteLine)
				;
		}
		

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new CountryIPv4EntityConfig());
			modelBuilder.ApplyConfiguration(new CountryLocationEntityConfig());
			modelBuilder.ApplyConfiguration(new CityIPv4EntityConfig());
			modelBuilder.ApplyConfiguration(new CityLocationEntityConfig());
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<CountryIPv4Entity> CountryIPv4s { get; set; }
		public DbSet<CountryLocationEntity> CountryLocations { get; set; }
		public DbSet<CityIPv4Entity> CityIPv4s { get; set; }
		public DbSet<CityLocationEntity> CityLocations { get; set; }
	}
}
