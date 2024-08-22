using Geo.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
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

			builder.HasOne(x => x.Geoname);
			builder.HasOne(x => x.RegisteredCountryGeoName);
			builder.HasOne(x => x.RepresentedCountryGeoName);
		}
	}
}
