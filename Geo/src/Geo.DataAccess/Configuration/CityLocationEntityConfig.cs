using Geo.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geo.DataAccess.Configuration
{
	public class CityLocationEntityConfig: IEntityTypeConfiguration<CityLocationEntity>
	{
		public void Configure(EntityTypeBuilder<CityLocationEntity> builder)
		{
			builder.HasKey(e => e.GeonameId);
		}
	}
}
