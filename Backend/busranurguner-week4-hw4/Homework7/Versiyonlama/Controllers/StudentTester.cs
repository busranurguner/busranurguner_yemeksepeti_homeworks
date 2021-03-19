using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Versiyonlama.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("1.1")]
    [ApiVersion("2.0")]
    public class StudentTester : ControllerBase
    {
        [HttpGet(Name = nameof(GetStudents))]
        public IActionResult GetStudents()
        {
            List<string> students = new List<string>()
            {
                "Büşra Güner",
                "Betül Güner"
            };

            return Ok(students);
        }

        [ApiVersion("1.0", Deprecated = true)]
        [MapToApiVersion("1.1")]
        [HttpGet(Name = nameof(GetStudentsV2))]
        public IActionResult GetStudentsV2()
        {
            List<string> students = new List<string>()
            {
                "Büşra",
                "Betül"
            };

            return Ok(students);
        }
    }
}
