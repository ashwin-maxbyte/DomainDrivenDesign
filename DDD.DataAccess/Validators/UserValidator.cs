using DDD.Base.DataManagement;
using DDD.Base.TransientModels;
using DDD.Base.Validators;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DDD.DataAccess.Validators
{
    public class UserValidator : AbstractUserValidator
    {
        private readonly IRepository _repository;

        public UserValidator(IRepository repository)
        {
            _repository = repository;
        }

        protected override bool ValidateMandatoryFirstName(TransientUser transientUser, long exclusionId)
        {
            return !string.IsNullOrEmpty(transientUser.FirstName);
        }

        protected override bool ValidatePasswordBasedOnSocialLogin(TransientUser transientEntity, long exclusionId)
        {
            bool isValidPasswordConfiguration = 
                (transientEntity.IsSocialLogin && transientEntity.Password == null) || 
                (!transientEntity.IsSocialLogin && transientEntity.Password != null);

            return isValidPasswordConfiguration;
        }

        protected override async Task<bool> ValidateUniqueEmail(TransientUser transientEntity, long exclusionId)
        {
            var doesEmailExist = await _repository.Users.AnyAsync(x => x.Email == transientEntity.Email && x.Id != exclusionId);
            return !doesEmailExist;
        }
    }
}
