using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Data.Configurations
{
    public class CaseConfiguration : IEntityTypeConfiguration<Case>
    {
        public void Configure(EntityTypeBuilder<Case> builder)
        {
            builder.ToTable("covid_observations");

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id")
                .IsRequired()
                .HasColumnType("integer")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            builder.Property(c => c.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired()
                .HasColumnType("timestamp")
                .HasDefaultValueSql("NOW() AT TIME ZONE 'utc'");

            builder.Property(c => c.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired()
                .HasColumnType("timestamp")
                .HasDefaultValueSql("NOW() AT TIME ZONE 'utc'");

            builder.Property(c => c.ObservationDate)
                .HasColumnName("observation_date")
                .IsRequired()
                .HasColumnType("timestamp")
                .HasDefaultValueSql("NOW() AT TIME ZONE 'utc'");

            builder.Property(c => c.ProvinceState)
                .HasColumnName("province_state")
                .HasColumnType("varchar")
                .HasMaxLength(256);

            builder.Property(c => c.CountryRegion)
                .HasColumnName("country_region")
                .HasColumnType("varchar")
                .HasMaxLength(256);

            builder.Property(c => c.Confirmed)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("confirmed")
                .HasDefaultValue(0.0);

            builder.Property(c => c.Deaths)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("deaths")
                .HasDefaultValue(0.0);

            builder.Property(c => c.Recovered)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("recovered")
                .HasDefaultValue(0.0);

            builder.HasKey(c => c.Id)
                .HasName("pk_orders_id");
        }
    }
}
