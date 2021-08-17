using DDDWebApi.Models.Auth;
using DDDWebApi.Models.User;
using System.Threading.Tasks;

namespace DDDWebApi.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateUser(CreateUserRequest createUserRequest);

        Task<string> SignupLogin(SignupLoginRequest signupLoginRequest);

        Task<string> Authenticate(AuthenticateRequest authenticateRequest);

        Task<string> AuthenticateExternal(AuthenticateExternalRequest authenticateExternalRequest);

        Task<bool> DoesEmailExist(string email);
    }
}
