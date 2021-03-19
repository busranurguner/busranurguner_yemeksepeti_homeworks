using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Versiyonlama.Controllers
{
    [Route("/")]
    [ApiController]
    [ApiVersion("2.0")]
    public class SchoolController : ControllerBase
    {

        [HttpGet(Name = nameof(GetSchool))]
        public IActionResult GetSchool()
        {
            var response = new
            {
                href = Url.Link(nameof(GetSchool), null),
                rooms = new
                {
                    href = Url.Link(nameof(ClassController.GetClass), null)
                }

            };

            return Ok(response);
        }

    }
}
