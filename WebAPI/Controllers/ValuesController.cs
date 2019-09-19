using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Bearded.Monads;
using WebAPI.Models;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            var result = new SomeDto { NumberParam = 1, StringParam = "str" };
            return Ok(result);
        }
    }
}
