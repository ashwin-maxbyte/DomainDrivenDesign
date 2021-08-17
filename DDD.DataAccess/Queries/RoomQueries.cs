using DDD.Base.DataManagement;
using DDD.Base.Extensions;
using DDD.Base.Models;
using DDD.Base.Queries;
using DDD.Base.Queries.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDD.DataAccess.Queries
{
    public class RoomQueries : IRoomQueries
    {
        private readonly IRepository _repository;

        public RoomQueries(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Room>> GetRooms(PaginationContext paginationContext)
        {
            return await _repository.Rooms.ApplyPagination(paginationContext).ToListAsync();
        }
    }
}
