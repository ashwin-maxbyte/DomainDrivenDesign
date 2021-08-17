using AutoMapper;
using DDD.Base.Models;
using DDD.Base.TransientModels;

namespace DDD.DataAccess.DataManagement
{
    internal class AutoMapperConfiguration
    {
        private readonly MapperConfiguration _mapperConfiguration;

        public AutoMapperConfiguration()
        {
            _mapperConfiguration = new(configuration => 
            {
                RegisterTransientToDomainMap(configuration);
            });
        }

        public IMapper Mapper => _mapperConfiguration.CreateMapper();

        private void RegisterTransientToDomainMap(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<TransientUser, User>();
            configuration.CreateMap<TransientConfiguration, Configuration>();
            configuration.CreateMap<TransientRoom, Room>();
        }
    }
}
