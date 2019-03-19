using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/canteens")]
    public class CanteenController : Controller
    {
        // GET: api/canteens
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/canteens/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
