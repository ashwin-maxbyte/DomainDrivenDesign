using DDD.Base.Extensions;
using DDD.Base.Queries.Pagination;
using DDD.Base.Validators;
using DDDWebApi.Models;
using DDDWebApi.Models.Room;
using DDDWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveRoom(SaveRoomRequest request)
        {
            try
            {
                var payload = await _roomService.SaveRoom(request);
                return Ok(GenericResponse<RoomResource>.GetSuccessResponse(payload));
            }
            catch (DataIntegrityException ex)
            {
                return BadRequest(GenericResponse<RoomResource>.GetFailureResponse(ex.Message));
            }
            catch (InvalidOperationException)
            {
                return BadRequest(GenericResponse<RoomResource>.GetFailureResponse(Messages.Room_SaveRoom_InvalidId));
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRoom(long id)
        {
            try
            {
                await _roomService.DeleteRoom(id);
                return Ok(GenericResponse<object>.GetSuccessResponse(null));
            }
            catch(InvalidOperationException)
            {
                return NotFound(GenericResponse<object>.GetFailureResponse(Messages.Room_DeleteRoom_InvalidId));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRooms(int? offset, int? limit)
        {
            try
            {
                var payload = await _roomService.GetRooms(offset, limit);
                return Ok(GenericResponse<IEnumerable<RoomResource>>.GetSuccessResponse(payload));
            }
            catch(InvalidPaginationContextException ex)
            {
                return BadRequest(GenericResponse<object>.GetFailureResponse(ex.Message));
            }
        }
    }
}
