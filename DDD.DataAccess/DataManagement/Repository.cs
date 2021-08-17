using DDD.Base.DataManagement;
using DDD.Base.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DDD.DataAccess.DataManagement
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<User> Users => GetActiveEntities(_dbContext.Users);

        public IQueryable<Configuration> Configurations => GetActiveEntities(_dbContext.Configurations);

        public IQueryable<Room> Rooms => GetActiveEntities(_dbContext.Rooms);

        private static IQueryable<T> GetActiveEntities<T>(DbSet<T> dbSet) where T : BaseEntity
        {
            return dbSet.AsQueryable().Where(x => x.IsActive);
        }
    }
}
