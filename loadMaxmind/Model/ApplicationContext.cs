using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace loadMaxmind.Model
{
    
    class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(Config.getInstans().GetConfigByName("ConnectionStrings"));

        public DbSet<CountryLocation> CountryLocations { get; set; }
        public DbSet<Ipv4bloc> Ipv4bloc { get; set; }
    }
    
}
