using DDD.Base.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.DataAccess.EntityConfigurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Email).HasColumnType("varchar").HasMaxLength(50).IsRequired();

            builder.Property(x => x.Password).HasColumnType("varchar").HasMaxLength(50);

            builder.Property(x => x.FirstName).HasColumnType("varchar").HasMaxLength(100).IsRequired();

            builder.Property(x => x.LastName).HasColumnType("varchar").HasMaxLength(50);

            builder.Property(x => x.IsSocialLogin).IsRequired();

            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasIndex(x => new { x.Email, x.Password });
        }
    }
}
