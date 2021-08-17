using AutoMapper;
using DDD.Base.DataManagement;
using DDD.Base.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DDD.DataAccess.DataManagement
{
    public partial class Transaction : AbstractTransaction
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public Transaction(AppDbContext dbContext, Base.Validators.ValidationContext validationContext) : base(validationContext)
        {
            _mapper = new AutoMapperConfiguration().Mapper;
            _dbContext = dbContext;
        }

        private async Task<TEntity> InsertEntity<TEntity, TTransientEntity>(DbSet<TEntity> dbSet, TTransientEntity transientEntity) where TEntity : BaseEntity where TTransientEntity : class
        {
            var baseEntity = _mapper.Map<TTransientEntity, TEntity>(transientEntity);
            PrepareBaseEntityForInsert(baseEntity);
            dbSet.Add(baseEntity);
            await _dbContext.SaveChangesAsync();
            return baseEntity;
        }

        private async Task<TEntity> UpdateEntity<TEntity, TTransientEntity>(DbSet<TEntity> dbSet, long id, TTransientEntity transientEntity) where TEntity : BaseEntity where TTransientEntity : class
        {
            var baseEntity = await dbSet.FirstAsync(x => x.Id == id && x.IsActive);
            _mapper.Map(transientEntity, baseEntity);
            PrepareBaseEntityForUpdate(baseEntity);
            dbSet.Update(baseEntity);
            await _dbContext.SaveChangesAsync();
            return baseEntity;
        }

        private async Task<T> DeleteEntity<T>(DbSet<T> dbSet, long id) where T : BaseEntity
        {
            var baseEntity = await dbSet.FirstAsync(x => x.Id == id && x.IsActive);
            PrepareBaseEntityForDelete(baseEntity);
            dbSet.Update(baseEntity);
            await _dbContext.SaveChangesAsync();
            return baseEntity;
        }

        private static void PrepareBaseEntityForInsert(BaseEntity baseEntity)
        {
            baseEntity.IsActive = true;
            baseEntity.CreatedOn = DateTime.Now;
            baseEntity.UpdatedOn = null;
        }

        private static void PrepareBaseEntityForUpdate(BaseEntity baseEntity)
        {
            baseEntity.UpdatedOn = DateTime.Now;
        }

        private static void PrepareBaseEntityForDelete(BaseEntity baseEntity)
        {
            baseEntity.IsActive = false;
            PrepareBaseEntityForUpdate(baseEntity);
        }
    }
}
