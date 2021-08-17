using DDD.Base.DataManagement;
using DDD.Base.Models;
using DDD.Base.TransientModels;
using System.Threading.Tasks;

namespace DDD.DataAccess.DataManagement
{
    public partial class Transaction : AbstractTransaction
    {
        protected override async Task<Room> UpdateRoomRaw(long id, TransientRoom transientRoom)
        {
            return await UpdateEntity(_dbContext.Rooms, id, transientRoom);
        }

        protected override async Task<Configuration> UpdateConfigurationRaw(long id, TransientConfiguration transientConfiguration)
        {
            return await UpdateEntity(_dbContext.Configurations, id, transientConfiguration);
        }
    }
}
