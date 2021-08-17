using DDD.Base.Models;
using DDD.Base.TransientModels;
using DDD.Base.Validators.ValidatorAnnotations;
using System.Threading.Tasks;

namespace DDD.Base.Validators
{
    [ValidatorEntity(nameof(Room))]
    public abstract class AbstractRoomValidator : BaseValidator<AbstractRoomValidator, TransientRoom>
    {
        [Validation(nameof(Room.Name), "Room Name is required")]
        protected abstract bool ValidateMandatoryRoomName(TransientRoom transientRoom, long exclusionId);

        [Validation(nameof(Room.Name), "Room Name must be Unique")]
        protected abstract Task<bool> ValidateUniqueRoomName(TransientRoom transientRoom, long exclusionId);
    }
}
