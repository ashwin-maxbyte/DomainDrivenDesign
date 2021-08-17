using DDD.Base.Models;
using DDD.Base.TransientModels;
using DDD.Base.Validators.ValidatorAnnotations;
using System.Threading.Tasks;

namespace DDD.Base.Validators
{
    [ValidatorEntity(nameof(Configuration))]
    public abstract class AbstractConfigurationValidator : BaseValidator<AbstractConfigurationValidator, TransientConfiguration>
    {
        [Validation(nameof(Configuration.UserId), "Invalid User Id")]
        protected abstract Task<bool> ValidateUserIdForeignKeyConstraint(TransientConfiguration transientConfiguration, long exclusionId);
    }
}
