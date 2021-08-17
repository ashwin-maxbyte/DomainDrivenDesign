using DDDWebApi.Models.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDWebApi.Services
{
    public interface IConfigurationService
    {
        Task<ConfigurationResource> SaveConfiguration(ConfigurationResource configurationResource, string jsonWebToken);

        Task<IEnumerable<ConfigurationResource>> GetConfigurations(string jsonWebToken, int? offset, int? limit);
    }
}
