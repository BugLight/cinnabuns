using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/canteens/{canteenId}/meals")]
    public class MealController : Controller
    {
        // GET: api/canteens/5/meals
        [HttpGet]
        public IEnumerable<string> Get(int canteenId)
        {
            return new string[] { canteenId.ToString() };
        }

        // GET api/canteens/5/meals/3
        [HttpGet("{id}")]
        public string Get(int canteenId, int id)
        {
            return canteenId.ToString() + " " + id.ToString();
        }
    }
}
