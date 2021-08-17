using DDD.Base.Models;
using DDD.DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DDD.DataAccess.DataManagement
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ConfigurationConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
        }
    }
}
