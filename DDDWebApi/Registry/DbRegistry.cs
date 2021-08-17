using DDD.Base.DataManagement;
using DDD.Base.Queries;
using DDD.Base.Validators;
using DDD.DataAccess.DataManagement;
using DDD.DataAccess.Queries;
using DDD.DataAccess.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DDDWebApi.Registry
{
    public class DbRegistry : IRegistry
    {
        private readonly IServiceCollection _services;
        private readonly string _connectionString;

        public DbRegistry(IServiceCollection services, string connectionString)
        {
            _services = services;
            _connectionString = connectionString;
        }

        public void Register()
        {
            RegisterDbContext();

            RegisterDataManagement();

            RegisterValidators();

            RegisterQueries();
        }

        private void RegisterDbContext()
        {
            _services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(_connectionString);
            });
        }

        private void RegisterDataManagement()
        {
            _services.AddScoped<IRepository, Repository>();
            _services.AddScoped<AbstractTransaction, Transaction>();
        }

        private void RegisterValidators()
        {
            _services.AddScoped<AbstractUserValidator, UserValidator>();
            _services.AddScoped<AbstractConfigurationValidator, ConfigurationValidator>();
            _services.AddScoped<AbstractRoomValidator, RoomValidator>();

            _services.AddScoped<ValidationContext>();
        }

        private void RegisterQueries()
        {
            _services.AddScoped<IUserQueries, UserQueries>();
            _services.AddScoped<IRoomQueries, RoomQueries>();
            _services.AddScoped<IConfigurationQueries, ConfigurationQueries>();
        }
    }
}
