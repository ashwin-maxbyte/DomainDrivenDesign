using DDDWebApi.Models.Room;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDWebApi.Services
{
    public interface IRoomService
    {
        Task<RoomResource> SaveRoom(SaveRoomRequest request);

        Task DeleteRoom(long roomId);

        Task<IEnumerable<RoomResource>> GetRooms(int? offset, int? limit);
    }
}
