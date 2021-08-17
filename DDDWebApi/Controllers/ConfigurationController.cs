using DDD.Base.Queries.Pagination;
using DDD.Base.Validators;
using DDDWebApi.Models;
using DDDWebApi.Models.Configuration;
using DDDWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService _configurationService;

        public ConfigurationController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateConfiguration([FromBody] ConfigurationResource configurationResource)
        {
            try
            {
                var payload = await _configurationService.SaveConfiguration(configurationResource, GetJsonWebToken());
                var response = GenericResponse<ConfigurationResource>.GetSuccessResponse(payload);
                return Ok(response);
            }
            catch(DataIntegrityException ex)
            {
                var response = GenericResponse<ConfigurationResource>.GetFailureResponse(ex.Message);
                return BadRequest(response);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetConfigurations(int? limit, int? offset)
        {
            try
            {
                var payload = await _configurationService.GetConfigurations(GetJsonWebToken(), offset, limit);
                return Ok(GenericResponse<IEnumerable<ConfigurationResource>>.GetSuccessResponse(payload));
            }
            catch(InvalidPaginationContextException ex)
            {
                return BadRequest(GenericResponse<object>.GetFailureResponse(ex.Message));
            }
        }

        private string GetJsonWebToken()
        {
            string authHeader = HttpContext.Request.Headers["Authorization"];
            string jsonWebToken = authHeader.Split(' ')[1];
            return jsonWebToken;
        }
    }
}
