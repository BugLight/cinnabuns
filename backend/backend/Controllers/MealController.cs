using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api")]
    public class MealController : Controller
    {
        // GET: api/canteens/5/meals
        [HttpGet("canteens/{canteenId}/meals")]
        public IEnumerable<string> GetCanteenMeals(int canteenId)
        {
            return new string[] { canteenId.ToString() };
        }

        // GET api/meals/3
        [HttpGet("meals/{id}")]
        public string GetMeal(int id)
        {
            return id.ToString();
        }
    }
}
