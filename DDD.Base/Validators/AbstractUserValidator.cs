using DDD.Base.Models;
using DDD.Base.TransientModels;
using DDD.Base.Validators.ValidatorAnnotations;
using System.Threading.Tasks;

namespace DDD.Base.Validators
{
    [ValidatorEntity(nameof(User))]
    public abstract class AbstractUserValidator : BaseValidator<AbstractUserValidator, TransientUser>
    {
        [Validation(nameof(User.Email), "Email already exists")]
        protected abstract Task<bool> ValidateUniqueEmail(TransientUser transientEntity, long exclusionId);

        [Validation(nameof(User.Password), "Invalid Social Login Password configuration")]
        protected abstract bool ValidatePasswordBasedOnSocialLogin(TransientUser transientEntity, long exclusionId);

        [Validation(nameof(User.FirstName), "First Name is Required")]
        protected abstract bool ValidateMandatoryFirstName(TransientUser transientUser, long exclusionId);
    }
}
