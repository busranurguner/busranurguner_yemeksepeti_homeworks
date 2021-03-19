﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefreshToken_Hw8.Controllers
{
    [Route("/")]
    [ApiController]
    public class RootController :ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var response = new
            {
                href = Url.Link(nameof(GetRoot), null),
                Info = new
                {
                    href = Url.Link(nameof(InfoController.GetInfo), null)
                },
                rooms = new
                {
                    href = Url.Link(nameof(RoomsController.GetRooms), null)
                }
            };

            return Ok(response);
        }


    }
}