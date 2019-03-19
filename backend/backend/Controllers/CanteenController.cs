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
        private readonly object[] canteens = new object[]
        {
            new
            {
                Id = 0,
                Name = "Столовая №1",
                Description = "Отличная столовая",
                AcceptCards = true
            },
            new
            {
                Id = 1,
                Name = "Столовая №2",
                Description = "Тоже отличная столовая",
                AcceptCards = false
            }
        };

        // GET: api/canteens
        [HttpGet]
        public IEnumerable<object> Get()
        {
            return canteens;
        }

        // GET api/canteens/5
        [HttpGet("{id}")]
        public ActionResult<object> Get(int id)
        {
            switch (id)
            {
            case 0:
            case 1:
                return canteens[id];
            default:
                return NotFound();
            }
        }
    }
}
