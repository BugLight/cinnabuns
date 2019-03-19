using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using backend.Models;

namespace backend.Controllers
{
    [Route("api")]
    public class MealController : Controller
    {
        private readonly AppContext context;

        public MealController(AppContext context)
        {
            this.context = context;
        }

        // GET: api/canteens/5/meals
        [HttpGet("canteens/{canteenId}/meals")]
        public IEnumerable<Meal> GetCanteenMeals(int canteenId, [FromQuery] string categories,
                                                 [FromQuery] int? priceMin, [FromQuery] int? priceMax,
                                                 [FromQuery] int? calorieMin, [FromQuery] int? calorieMax)
        {

            return (from m in context.Meals
                    join c in context.MealCategories on m.MealCategoryId equals c.Id
                    where c.CanteenId == canteenId &&
                          (string.IsNullOrEmpty(categories) || categories.Contains(m.MealCategoryId.ToString())) &&
                          (priceMin == null || priceMin <= m.Price) &&
                          (priceMax == null || priceMax >= m.Calorie) &&
                          (calorieMin == null || calorieMin <= m.Calorie) &&
                          (calorieMax == null || calorieMax >= m.Calorie)
                    select m);
        }

        // GET api/meals/3
        [HttpGet("meals/{id}")]
        public ActionResult<Meal> GetMeal(int id)
        {
            var meal = context.Meals.Find(id);
            if (meal == null)
                return NotFound();

            return meal;
        }
    }
}
