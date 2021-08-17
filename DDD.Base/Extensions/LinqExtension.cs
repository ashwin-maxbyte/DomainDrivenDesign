using DDD.Base.Queries.Pagination;
using System.Linq;

namespace DDD.Base.Extensions
{
    public static class LinqExtension
    {
        public static IQueryable<TEntity> ApplyPagination<TEntity>(this IQueryable<TEntity> query, PaginationContext paginationContext)
        {
            if(paginationContext == null)
            {
                return query;
            }
            else
            {
                return query.Skip(paginationContext.Offset).Take(paginationContext.Limit);
            }
        }
    }
}
