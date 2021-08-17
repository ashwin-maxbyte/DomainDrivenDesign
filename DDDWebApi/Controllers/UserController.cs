using DDD.Base.Validators;
using DDDWebApi.Models;
using DDDWebApi.Models.User;
using DDDWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DDDWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest createUserRequest)
        {
            try
            {
                var payload = await _userService.CreateUser(createUserRequest);
                var response = GenericResponse<CreateUserResponse>.GetSuccessResponse(payload);
                return Ok(response);
            }
            catch (DataIntegrityException ex)
            {
                var response = GenericResponse<CreateUserResponse>.GetFailureResponse(ex.Message);
                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("email/{email}")]
        public async Task<IActionResult> DoesEmailExist(string email)
        {
            var payload = await _userService.DoesEmailExist(email);
            return Ok(GenericResponse<bool>.GetSuccessResponse(payload));
        }

        [HttpPost]
        [Route("signup-login")]
        public async Task<IActionResult> SignupLogin([FromBody] SignupLoginRequest signupLoginRequest)
        {
            try
            {
                var payload = new SignupLoginResponse 
                {
                    JsonWebToken = await _userService.SignupLogin(signupLoginRequest)
                };
                var response = GenericResponse<SignupLoginResponse>.GetSuccessResponse(payload);
                return Ok(response);
            }
            catch (DataIntegrityException ex)
            {
                var response = GenericResponse<CreateUserResponse>.GetFailureResponse(ex.Message);
                return BadRequest(response);
            }
        }
    }
}
