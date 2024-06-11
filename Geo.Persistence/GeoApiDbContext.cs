using Geo.Application.Interfaces;
using Geo.Domain.Models;
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
		
	}
}
