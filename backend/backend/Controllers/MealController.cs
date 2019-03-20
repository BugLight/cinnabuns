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
        private readonly AppContext context;

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
        public ActionResult<Meal> GetMeal(int id)
        {
            var meal = context.Meals.Find(id);
            if (meal == null)
                return NotFound();

            return meal;
        }

        [HttpPost("meals")]
        public ActionResult<Meal> AddMeal([FromBody] Meal meal)
        {
            if (meal == null)
                return BadRequest();

            context.Meals.Add(meal);
            context.SaveChanges();

            return meal;
        }

        [HttpPut("meals")]
        public ActionResult<Meal> EditMeal([FromBody] Meal newMeal)
        {
            if (newMeal == null)
                return BadRequest();

            var meal = context.Meals.Find(newMeal.Id);

            if (meal == null)
                return NotFound();

            meal.Name = newMeal.Name;
            meal.Price = newMeal.Price;
            meal.Weight = newMeal.Weight;
            meal.Calorie = newMeal.Calorie;
            meal.MealCategory = newMeal.MealCategory;
            meal.MealCategoryId = newMeal.MealCategoryId;
            context.SaveChanges();

            return meal;
        }

        [HttpDelete("meals/{id}")]
        public ActionResult DeleteMeal(int id)
        {
            var meal = context.Meals.Find(id);

            if (meal == null)
                return NotFound();

            context.Meals.Remove(meal);
            context.SaveChanges();

            return Ok();
        }
    }
}
