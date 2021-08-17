using DDD.Base.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DDD.DataAccess.EntityConfigurations
{
    public class ConfigurationConfiguration : BaseEntityConfiguration<Configuration>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Configuration> builder)
        {
            builder.ToTable("Configurations");

            builder.Property(x => x.UserId).HasColumnType("bigint").IsRequired();

            builder.Property(x => x.RoomTemperature).HasColumnType("decimal").HasPrecision(4, 2).IsRequired();

            builder.Property(x => x.Area).HasColumnType("decimal").HasPrecision(38, 2).IsRequired();

            builder.Property(x => x.CeilingHeight).HasColumnType("decimal").HasPrecision(38, 2).IsRequired();

            builder.Property(x => x.CurrentRating).HasColumnType("decimal").HasPrecision(38, 2).IsRequired();

            builder.HasOne(x => x.User);

            builder.HasIndex(x => x.UserId);
        }
    }
}
