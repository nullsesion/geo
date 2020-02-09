using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace loadMaxmind.Model
{

    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<CountryLocation> CountryLocations { get; set; }
        public DbSet<Ipv4bloc> Ipv4bloc { get; set; }
    }

}
