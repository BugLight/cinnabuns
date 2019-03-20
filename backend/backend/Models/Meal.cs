namespace backend.Models
{
    public class Meal
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Weight { get; set; }
        public int Calorie { get; set; }
        public decimal Price { get; set; }

        public int MealCategoryId { get; set; }
        public MealCategory MealCategory { get; set; }
    }
}
