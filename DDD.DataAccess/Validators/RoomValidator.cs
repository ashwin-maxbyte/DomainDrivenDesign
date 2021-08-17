using DDD.Base.DataManagement;
using DDD.Base.TransientModels;
using DDD.Base.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.DataAccess.Validators
{
    public class RoomValidator : AbstractRoomValidator
    {
        private readonly IRepository _repository;

        public RoomValidator(IRepository repository)
        {
            _repository = repository;
        }

        protected override bool ValidateMandatoryRoomName(TransientRoom transientRoom, long exclusionId)
        {
            return !string.IsNullOrEmpty(transientRoom.Name);
        }

        protected override async Task<bool> ValidateUniqueRoomName(TransientRoom transientRoom, long exclusionId)
        {
            return await _repository.Rooms.Where(x => x.Id != exclusionId).AllAsync(x => x.Name != transientRoom.Name);
        }
    }
}
