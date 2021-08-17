using AutoMapper;
using DDD.Base.DataManagement;
using DDD.Base.Models;
using DDD.Base.Queries;
using DDD.Base.Queries.Pagination;
using DDD.Base.TransientModels;
using DDDWebApi.Models.Room;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDWebApi.Services
{
    public class RoomService : IRoomService
    {
        private readonly AbstractTransaction _transaction;
        private readonly IRoomQueries _roomQueries;
        private readonly IMapper _mapper;

        public RoomService(AbstractTransaction transaction, IRoomQueries roomQueries, IMapper mapper)
        {
            _transaction = transaction;
            _roomQueries = roomQueries;
            _mapper = mapper;
        }

        public async Task<RoomResource> SaveRoom(SaveRoomRequest request)
        {
            Room room;
            TransientRoom transientRoom = _mapper.Map<TransientRoom>(request);

            if (request.Id.HasValue)
            {
                room = await _transaction.UpdateRoom(request.Id.Value, transientRoom);
            }
            else
            {
                room = await _transaction.InsertRoom(transientRoom);
            }

            return _mapper.Map<RoomResource>(room);
        }

        public async Task DeleteRoom(long roomId)
        {
            await _transaction.DeleteRoom(roomId);
        }

        public async Task<IEnumerable<RoomResource>> GetRooms(int? offset, int? limit)
        {
            PaginationContext paginationContext = PaginationContext.NewPaginationContext(offset, limit);
            var rooms = await _roomQueries.GetRooms(paginationContext);
            return rooms.Select(x => _mapper.Map<RoomResource>(x));
        }
    }
}
