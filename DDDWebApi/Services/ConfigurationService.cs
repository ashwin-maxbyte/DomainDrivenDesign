using AutoMapper;
using DDD.Base.DataManagement;
using DDD.Base.Models;
using DDD.Base.Queries;
using DDD.Base.Queries.Pagination;
using DDD.Base.TransientModels;
using DDDWebApi.Helpers;
using DDDWebApi.Models.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDWebApi.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly AbstractTransaction _transaction;
        private readonly IJwtHelper _jwtHelper;
        private readonly IUserQueries _userQueries;
        private readonly IConfigurationQueries _configurationQueries;
        private readonly IMapper _mapper;

        public ConfigurationService(AbstractTransaction transaction, IJwtHelper jwtHelper, IUserQueries userQueries, IConfigurationQueries configurationQueries, IMapper mapper)
        {
            _transaction = transaction;
            _jwtHelper = jwtHelper;
            _userQueries = userQueries;
            _configurationQueries = configurationQueries;
            _mapper = mapper;
        }

        public async Task<ConfigurationResource> SaveConfiguration(ConfigurationResource configurationResource, string jsonWebToken)
        {
            long userId = await GetUserId(jsonWebToken);

            TransientConfiguration transientConfiguration = _mapper.Map<TransientConfiguration>(configurationResource);
            transientConfiguration.UserId = userId;

            Configuration configuration;
            if (configurationResource.Id.HasValue)
            {
                configuration = await _transaction.UpdateConfiguration(configurationResource.Id.Value, transientConfiguration);
            }
            else
            {
                configuration = await _transaction.InsertConfiguration(transientConfiguration);
            }

            ConfigurationResource response = _mapper.Map<ConfigurationResource>(configuration);
            return response;
        }

        public async Task<IEnumerable<ConfigurationResource>> GetConfigurations(string jsonWebToken, int? offset, int? limit)
        {
            long userId = await GetUserId(jsonWebToken);
            var paginationContext = PaginationContext.NewPaginationContext(offset, limit);
            var configurations = await _configurationQueries.GetConfigurations(userId, paginationContext);
            return configurations.Select(configuration => _mapper.Map<ConfigurationResource>(configuration));
        }

        private async Task<long> GetUserId(string jsonWebtoken)
        {
            Dictionary<string, string> claims = _jwtHelper.GetClaims(jsonWebtoken);
            string email = claims["email"];
            User user = await _userQueries.GetUser(email);
            return user.Id;
        }
    }
}
