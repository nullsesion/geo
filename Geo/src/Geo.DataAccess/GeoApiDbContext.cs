using Geo.Application.Interfaces;
using Geo.DataAccess.Configuration;
using Geo.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Geo.DataAccess
{
	public class GeoApiDbContext : DbContext, IGeoApiDbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=geo2;User ID=postgres;Password=postgres")
				//.LogTo(Console.WriteLine)
				;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new CountryIPv4EntityConfig());
			modelBuilder.ApplyConfiguration(new CountryLocationEntityConfig());
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<CountryIPv4Entity> CountryIPv4s { get; set; }
		public DbSet<CountryLocationEntity> CountryLocations { get; set; }
	}
}
