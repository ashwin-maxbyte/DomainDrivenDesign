using DDD.Base.DataManagement;
using DDD.Base.TransientModels;
using DDD.Base.Validators;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DDD.DataAccess.Validators
{
    public class ConfigurationValidator : AbstractConfigurationValidator
    {
        private readonly IRepository _repository;

        public ConfigurationValidator(IRepository repository)
        {
            _repository = repository;
        }

        protected override async Task<bool> ValidateUserIdForeignKeyConstraint(TransientConfiguration transientConfiguration, long exclusionId)
        {
            return await _repository.Users.AnyAsync(x => x.Id == transientConfiguration.UserId);
        }
    }
}
