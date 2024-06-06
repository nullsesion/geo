using Geo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geo.Persistence.EntityConfigurations
{
	public class IpLocationConfiguration: IEntityTypeConfiguration<IpLocation>
	{
		public void Configure(EntityTypeBuilder<IpLocation> builder)
		{
			builder.HasKey(ipLocation => ipLocation.Id);
			builder.Property(ipLocation => ipLocation.Address)
				.IsRequired()
				.HasMaxLength(50);
			builder.Property(ipLocation => ipLocation.Network)
				.IsRequired()
				.HasMaxLength(50);
			builder.Property(ipLocation => ipLocation.CountryCode)
				.IsRequired()
				.HasMaxLength(10);
			builder.Property(ipLocation => ipLocation.CountryName)
				.IsRequired()
				.HasMaxLength(1000);
		}
	}
}
