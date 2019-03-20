using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using Microsoft.EntityFrameworkCore;

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

            return (from m in context.Meals.Include(m => m.MealCategory)
                    where m.MealCategory.CanteenId == canteenId &&
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
            var meal = context.Meals.Include(m => m.MealCategory).FirstOrDefault(m => m.Id == id);
            if (meal == null)
                return NotFound();

            return meal;
        }
    }
}
