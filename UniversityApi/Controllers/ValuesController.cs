using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var data = new[] { "value1 ", "value2" };
            return Ok(data);

        }
        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var data = new[] { "value1" };
            return Ok(data);
        }
    }
}
