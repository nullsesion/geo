using Geo.Application.Interfaces;
using Geo.Domain;
using Geo.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Geo.Persistence
{
	public class GeoApiDbContext: DbContext, IGeoApiDbContext
	{
		public DbSet<IpLocation> IpLocations { get; set; }
		public Task<Guid> SaveChangesAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public GeoApiDbContext(DbContextOptions options) : base(options) { }

		public GeoApiDbContext() { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new IpLocationConfiguration());
			base.OnModelCreating(builder);
		}
		
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Database=GeoLocation;User ID=postgresql;Password=postgresql")
				//.LogTo(Console.WriteLine)
				;
		}
		
	}
}
