using GeoLoad.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geo.DataAccess.Configuration
{
	public class CityIPv4EntityConfig : IEntityTypeConfiguration<CityIPv4Entity>
	{
		public void Configure(EntityTypeBuilder<CityIPv4Entity> builder)
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
