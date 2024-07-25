using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geo.DataAccess.Configuration
{
	public class CountryLocationEntityConfig: IEntityTypeConfiguration<CountryLocationEntity>
	{
		public void Configure(EntityTypeBuilder<CountryLocationEntity> builder)
		{
			builder.HasKey(x => x.GeonameId);
			builder.Property(x => x.GeonameId).IsRequired();
			builder.Property(x => x.CountryName).HasColumnType("jsonb");
			builder.Property(x => x.ContinentName).HasColumnType("jsonb");
		}
	}
}
