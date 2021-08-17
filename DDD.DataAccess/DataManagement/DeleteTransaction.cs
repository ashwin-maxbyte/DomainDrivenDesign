using DDD.Base.DataManagement;
using DDD.Base.Models;
using System.Threading.Tasks;

namespace DDD.DataAccess.DataManagement
{
    public partial class Transaction : AbstractTransaction
    {
        public override async Task<Room> DeleteRoom(long id)
        {
            return await DeleteEntity(_dbContext.Rooms, id);
        }
    }
}
