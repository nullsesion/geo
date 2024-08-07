﻿// <auto-generated />
using System;
using Geo.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

#nullable disable

namespace Geo.DataAccess.Migrations
{
    [DbContext(typeof(GeoApiDbContext))]
    partial class GeoApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Geo.DataAccess.Entities.CityLocationEntity", b =>
                {
                    b.Property<int>("GeonameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GeonameId"));

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContinentCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContinentName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CountryIsoCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsInEuropeanUnion")
                        .HasColumnType("boolean");

                    b.Property<string>("LocaleCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("MetroCode")
                        .HasColumnType("integer");

                    b.Property<string>("Subdivision")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TimeZone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("GeonameId");

                    b.ToTable("CityLocations");
                });

            modelBuilder.Entity("Geo.DataAccess.Entities.CountryIPv4Entity", b =>
                {
                    b.Property<string>("Network")
                        .HasColumnType("text");

                    b.Property<int?>("GeonameId")
                        .HasColumnType("integer");

                    b.Property<int>("IpMax")
                        .HasColumnType("integer");

                    b.Property<int>("IpMin")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAnonymousProxy")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsAnycast")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSatelliteProvider")
                        .HasColumnType("boolean");

                    b.Property<int?>("RegisteredCountryGeoNameId")
                        .HasColumnType("integer");

                    b.Property<int?>("RepresentedCountryGeoNameId")
                        .HasColumnType("integer");

                    b.HasKey("Network");

                    b.HasIndex("IpMax");

                    b.HasIndex("IpMin");

                    b.ToTable("CountryIPv4s");
                });

            modelBuilder.Entity("Geo.DataAccess.Entities.CountryLocationEntity", b =>
                {
                    b.Property<int>("GeonameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GeonameId"));

                    b.Property<string>("ContinentCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContinentName")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<string>("CountryIsoCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<bool>("IsInEuropeanUnion")
                        .HasColumnType("boolean");

                    b.HasKey("GeonameId");

                    b.ToTable("CountryLocations");
                });

            modelBuilder.Entity("GeoLoad.Entities.CityIPv4Entity", b =>
                {
                    b.Property<string>("Network")
                        .HasColumnType("text");

                    b.Property<int?>("AccuracyRadius")
                        .HasColumnType("integer");

                    b.Property<int?>("GeonameId")
                        .HasColumnType("integer");

                    b.Property<int>("IpMax")
                        .HasColumnType("integer");

                    b.Property<int>("IpMin")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAnonymousProxy")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsAnycast")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSatelliteProvider")
                        .HasColumnType("boolean");

                    b.Property<NpgsqlPoint?>("Location")
                        .HasColumnType("point");

                    b.Property<int?>("RegisteredCountryGeoNameId")
                        .HasColumnType("integer");

                    b.Property<int?>("RepresentedCountryGeoNameId")
                        .HasColumnType("integer");

                    b.HasKey("Network");

                    b.HasIndex("IpMax");

                    b.HasIndex("IpMin");

                    b.ToTable("CityIPv4s");
                });
#pragma warning restore 612, 618
        }
    }
}
