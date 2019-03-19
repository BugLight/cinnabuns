using System.Collections.Generic;
using System.Linq;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/canteens")]
    public class CanteenController : Controller
    {
        private readonly AppContext context;

        public CanteenController(AppContext context)
        {
            this.context = context;
        }

        // GET: api/canteens
        [HttpGet]
        public IEnumerable<Canteen> GetCanteens()
        {
            return from c in context.Canteens select c;
        }

        // GET api/canteens/5
        [HttpGet("{id}")]
        public ActionResult<object> Get(int id)
        {
            var canteen = context.Canteens.Find(id);
            if (canteen == null)
                return NotFound();
            return canteen;
        }
    }
}
