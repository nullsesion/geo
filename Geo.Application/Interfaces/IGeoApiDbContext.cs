using Geo.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Geo.Application.Interfaces
{
    public interface IGeoApiDbContext
	{
		public DbSet<IpLocation> IpLocations { get; set; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
 