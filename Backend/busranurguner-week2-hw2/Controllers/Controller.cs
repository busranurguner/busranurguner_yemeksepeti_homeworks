using diet_center.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_center.Mapping;
using diet_center.RequestModel;
using Newtonsoft.Json;

namespace diet_center.Controllers
{
    public class Controller
    {
        [ApiController]
        [Route("api/[controller]")]
        public class PatientController : ControllerBase
        {

            private DbContextOptions<DatabaseContext> option;

            public PatientController()
            {
                option = new DbContextOptionsBuilder<DatabaseContext>()
                       .UseInMemoryDatabase(databaseName: "TestDatabase")
                       .Options;

            }


            [HttpGet]
            public IActionResult Get()
            {
                List<PatientModel> result = new List<PatientModel>();

                using (DatabaseContext dbContext = new DatabaseContext(option))
                {
                    var entityList = dbContext.Patients.ToList();
                    result = entityList.ToPatientResponse();
                }

                return Ok(result);
            }

            [HttpPost]
            
            public IActionResult Post([FromBody] Model1 request)
            {
                var request2 = JsonConvert.DeserializeObject<Model1>(JsonConvert.SerializeObject(request));

                var validate2_0 = request2.Validate();
                if (validate2_0.Item1)
                    return BadRequest(validate2_0.Item2);

                var validate2_1 = request2.Validate2();
                if (!validate2_1.isValid)
                    return BadRequest(validate2_1.validationErrors);

                return Ok();
            }
        }
    }
}
