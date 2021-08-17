using AutoMapper;
using DDD.Base.Models;
using DDD.Base.TransientModels;
using DDDWebApi.Models.Configuration;
using DDDWebApi.Models.Room;
using DDDWebApi.Models.User;

namespace DDDWebApi.Helpers.AutoMapperProfiles
{
    public class ApiResourceToEntityProfile : Profile
    {
        public ApiResourceToEntityProfile()
        {
            CreateMap<SaveRoomRequest, TransientRoom>();
            CreateMap<Room, RoomResource>();

            CreateMap<ConfigurationResource, TransientConfiguration>();
            CreateMap<Configuration, ConfigurationResource>();

            CreateMap<User, CreateUserResponse>();
            CreateMap<SignupLoginRequest, TransientUser>();
        }
    }
}
