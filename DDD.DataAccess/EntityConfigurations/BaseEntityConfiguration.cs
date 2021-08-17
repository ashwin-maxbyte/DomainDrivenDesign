using DDD.Base.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.DataAccess.EntityConfigurations
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            ConfigureEntity(builder);

            ConfigureBaseEntity(builder);
        }

        private void ConfigureBaseEntity(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Id).HasColumnType("BigInt");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.IsActive).HasColumnType("Bit").IsRequired();

            builder.Property(x => x.CreatedOn).HasColumnType("DateTimeOffset").IsRequired();

            builder.Property(x => x.UpdatedOn).HasColumnType("DateTimeOffset");
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
    }
}
