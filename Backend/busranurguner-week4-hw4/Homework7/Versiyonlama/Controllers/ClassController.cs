using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Versiyonlama.Controllers
{
    //[Authorize]
    [Route("/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {

        [HttpGet(Name = nameof(GetClass))]
        public async Task<IActionResult> GetClass()
        {
            throw new NotImplementedException();

        }

    }
}
