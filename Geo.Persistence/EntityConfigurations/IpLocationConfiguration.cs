using Geo.Domain.Models;
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
				.HasMaxLength(IpLocation.MAX_LEN_ADDRESS);
			builder.Property(ipLocation => ipLocation.Network)
				.IsRequired()
				.HasMaxLength(IpLocation.MAX_LEN_NETWORK);
			builder.Property(ipLocation => ipLocation.CountryCode)
				.IsRequired()
				.HasMaxLength(IpLocation.MAX_LEN_COUNTRYCODE);
			builder.Property(ipLocation => ipLocation.CountryName)
				.IsRequired()
				.HasMaxLength(1000);
		}
	}
}
