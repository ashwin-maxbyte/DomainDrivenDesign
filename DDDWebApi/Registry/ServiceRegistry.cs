using DDDWebApi.Helpers;
using DDDWebApi.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DDDWebApi.Registry
{
    public class ServiceRegistry : IRegistry
    {
        private readonly IServiceCollection _services;

        public ServiceRegistry(IServiceCollection services)
        {
            _services = services;
        }

        public void Register()
        {
            RegisterServices();

            RegisterHelpers();
        }

        private void RegisterServices()
        {
            _services.AddScoped<IUserService, UserService>();
            _services.AddScoped<IConfigurationService, ConfigurationService>();
            _services.AddScoped<IRoomService, RoomService>();
        }

        private void RegisterHelpers()
        {
            _services.AddScoped<IJwtHelper, JwtHelper>();
            _services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
