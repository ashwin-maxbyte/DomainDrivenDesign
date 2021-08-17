using DDD.Base.Models;
using DDD.Base.Queries.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDD.Base.Queries
{
    public interface IConfigurationQueries
    {
        Task<IEnumerable<Configuration>> GetConfigurations(long userId, PaginationContext paginationContext);
    }
}
