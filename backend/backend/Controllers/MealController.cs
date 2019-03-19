using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using backend.Models;

namespace backend.Controllers
{
    [Route("api")]
    public class MealController : Controller
    {
        public readonly AppContext context;

        // GET: api/canteens/5/meals
        [HttpGet("canteens/{canteenId}/meals")]
        public IEnumerable<Meal> GetCanteenMeals(int? canteenId, int? mealCategoryId, int? priceMin, int? priceMax, int? calorieMin, int? calorieMax)
        {
            return (from m in context.Meals
                    join c in context.MealCategories on m.MealCategoryId equals c.Id
                    where (c.CanteenId == canteenId &&
                         (mealCategoryId == null || m.MealCategoryId == mealCategoryId) &&
                         (priceMin == null || priceMin <= m.Price) &&
                         (priceMax == null || priceMax >= m.Calorie) &&
                         (calorieMin == null || calorieMin <= m.Calorie) &&
                         (calorieMax == null || calorieMax >= m.Calorie))
                    select m);
        }

        // GET api/meals/3
        [HttpGet("meals/{id}")]
        public string GetMeal(int id)
        {
            return id.ToString();
        }
    }
}
