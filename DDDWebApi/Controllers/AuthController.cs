using DDDWebApi.Models;
using DDDWebApi.Models.Auth;
using DDDWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DDDWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest request)
        {
            var jsonWebToken = await _userService.Authenticate(request);
            if(string.IsNullOrEmpty(jsonWebToken))
            {
                var response = GenericResponse<AuthenticateResponse>.GetFailureResponse(Messages.Auth_Authenticate_Fail);
                return Unauthorized(response);
            }
            else
            {
                var payload = new AuthenticateResponse
                {
                    JsonWebToken = jsonWebToken
                };
                var response = GenericResponse<AuthenticateResponse>.GetSuccessResponse(payload);
                return Ok(response);
            }
        }

        [HttpPost]
        [Route("external")]
        public async Task<IActionResult> AuthenticateExternal([FromBody] AuthenticateExternalRequest request)
        {
            string jsonWebToken = await _userService.AuthenticateExternal(request);
            if (string.IsNullOrEmpty(jsonWebToken))
            {
                var response = GenericResponse<AuthenticateExternalResponse>.GetFailureResponse(Messages.Auth_Authenticate_Fail);
                return Unauthorized(response);
            }
            else
            {
                var payload = new AuthenticateExternalResponse
                {
                    JsonWebToken = jsonWebToken
                };
                var response = GenericResponse<AuthenticateExternalResponse>.GetSuccessResponse(payload);
                return Ok(response);
            }
        }
    }
}
