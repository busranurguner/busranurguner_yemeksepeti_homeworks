using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RefreshToken_Hw8.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefreshToken_Hw8.Controllers
{
    [Authorize]
    [Route("/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {

        private readonly IRoomService _roomService;
        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet(Name = nameof(GetRooms))]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _roomService.GetRoomsAsync();

            if (rooms == null)
                return NoContent();

            return Ok(rooms);

        }

    }
}
