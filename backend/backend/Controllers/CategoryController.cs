using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using backend.Models;

namespace backend.Controllers
{
    [Route("api")]
    public class CategoryController : Controller
    {
        private readonly AppContext context;

        // GET: api/canteens/5/categories
        [HttpGet("canteens/{canteenId}/categories")]
        public IEnumerable<MealCategory> GetCanteenCategories(int canteenId)
        {
            return (from m in context.MealCategories
                    where (m.CanteenId == canteenId)
                    select m);
        }

        // GET api/categories/3
        [HttpGet("categories/{id}")]
        public ActionResult<MealCategory> GetCategory(int id)
        {
            var mealCategory = context.MealCategories.Find(id);
            if (mealCategory == null)
                return NotFound();

            return mealCategory;
        }
    }
}
