using Geo.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geo.DataAccess.Configuration
{
	public class CountryIPv4EntityConfig : IEntityTypeConfiguration<CountryIPv4Entity>
	{
		public void Configure(EntityTypeBuilder<CountryIPv4Entity> builder)
		{
			builder.HasKey(e => e.Network);
			builder.HasIndex(e => e.IpMin);
			builder.HasIndex(e => e.IpMax);

			builder.Property(x => x.Network).IsRequired();
			builder.Property(x => x.IpMax).IsRequired();
			builder.Property(x => x.IpMin).IsRequired();
		}
	}
}
