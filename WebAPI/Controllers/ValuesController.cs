using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Bearded.Monads;
using WebAPI.Models;
using Newtonsoft.Json;
using WebAPI.Services;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IValuesService _valuesService;

        public ValuesController(IValuesService valuesService)
        {
            _valuesService = valuesService;
        }
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            var result = _valuesService.GetSomeDto();
            return Ok(result);
        }
    }
}
