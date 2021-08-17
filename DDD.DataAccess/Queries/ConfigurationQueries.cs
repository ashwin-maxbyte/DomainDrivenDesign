using DDD.Base.DataManagement;
using DDD.Base.Extensions;
using DDD.Base.Models;
using DDD.Base.Queries;
using DDD.Base.Queries.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.DataAccess.Queries
{
    public class ConfigurationQueries : IConfigurationQueries
    {
        private readonly IRepository _repository;

        public ConfigurationQueries(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Configuration>> GetConfigurations(long userId, PaginationContext paginationContext)
        {
            return await _repository.Configurations.Where(x => x.UserId == userId).ApplyPagination(paginationContext).ToListAsync();
        }
    }
}
