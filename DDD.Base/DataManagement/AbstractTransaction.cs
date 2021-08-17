using DDD.Base.Models;
using DDD.Base.TransientModels;
using DDD.Base.Validators;
using System.Threading.Tasks;

namespace DDD.Base.DataManagement
{
    public abstract class AbstractTransaction
    {
        private readonly ValidationContext _validationContext;

        protected AbstractTransaction(ValidationContext validationContext)
        {
            _validationContext = validationContext;
        }

        public async Task<User> InsertUser(TransientUser transientUser)
        {
            await _validationContext.UserValidator.Validate(transientUser, 0);
            return await InsertUserRaw(transientUser);
        }

        public async Task<Configuration> InsertConfiguration(TransientConfiguration transientConfiguration) 
        {
            await _validationContext.ConfigurationValidator.Validate(transientConfiguration, 0);
            return await InsertConfigurationRaw(transientConfiguration);
        }

        public async Task<Room> InsertRoom(TransientRoom transientRoom) 
        {
            await _validationContext.RoomValidator.Validate(transientRoom, 0);
            return await InsertRoomRaw(transientRoom);
        }

        public async Task<Configuration> UpdateConfiguration(long id, TransientConfiguration transientConfiguration)
        {
            await _validationContext.ConfigurationValidator.Validate(transientConfiguration, id);
            return await UpdateConfigurationRaw(id, transientConfiguration);
        }

        public async Task<Room> UpdateRoom(long id, TransientRoom transientRoom)
        {
            await _validationContext.RoomValidator.Validate(transientRoom, id);
            return await UpdateRoomRaw(id, transientRoom);
        }

        protected abstract Task<User> InsertUserRaw(TransientUser transientUser);

        protected abstract Task<Configuration> InsertConfigurationRaw(TransientConfiguration transientConfiguration);

        protected abstract Task<Room> InsertRoomRaw(TransientRoom transientRoom);

        protected abstract Task<Configuration> UpdateConfigurationRaw(long id, TransientConfiguration transientConfiguration);

        protected abstract Task<Room> UpdateRoomRaw(long id, TransientRoom transientRoom);

        public abstract Task<Room> DeleteRoom(long id);
    }
}
