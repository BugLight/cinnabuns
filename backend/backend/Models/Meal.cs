using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Meal
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public uint Weight { get; set; }
        public uint Calorie { get; set; }
        public decimal Price { get; set; }

        public int MealCategoryId { get; set; }
        public MealCategory MealCategory { get; set; }
    }
}
