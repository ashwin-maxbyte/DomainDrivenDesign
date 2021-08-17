using DDD.Base.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.DataAccess.EntityConfigurations
{
    public class RoomConfiguration : BaseEntityConfiguration<Room>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");

            builder.Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
