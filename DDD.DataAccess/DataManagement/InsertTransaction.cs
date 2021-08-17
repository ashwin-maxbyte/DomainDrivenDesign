using DDD.Base.DataManagement;
using DDD.Base.Models;
using DDD.Base.TransientModels;
using System.Threading.Tasks;

namespace DDD.DataAccess.DataManagement
{
    public partial class Transaction : AbstractTransaction
    {
        protected override async Task<User> InsertUserRaw(TransientUser transientUser)
        {
            return await InsertEntity(_dbContext.Users, transientUser);
        }

        protected override async Task<Configuration> InsertConfigurationRaw(TransientConfiguration transientConfiguration)
        {
            return await InsertEntity(_dbContext.Configurations, transientConfiguration);
        }

        protected override async Task<Room> InsertRoomRaw(TransientRoom transientRoom)
        {
            return await InsertEntity(_dbContext.Rooms, transientRoom);
        }
    }
}
