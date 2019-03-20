using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using backend.Models;

namespace backend.Controllers
{
    [Route("api")]
    public class CategoryController : Controller
    {
        private readonly AppContext context;

        public CategoryController(AppContext context)
        {
            this.context = context;
        }

        // GET: api/canteens/5/categories
        // Function for getting canteen's categories of meals by canteen id
        [HttpGet("canteens/{canteenId}/categories")]
        public IEnumerable<MealCategory> GetCanteenCategories(int canteenId)
        {
            return (from m in context.MealCategories
                    where m.CanteenId == canteenId
                    select m);
        }

        // GET: api/categories/3
        // Function for getting category of meals by category id
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
