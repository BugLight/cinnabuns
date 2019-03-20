namespace backend.Models
{
    public class Meal
    {
        // Primary key
        public int Id { get; set; }

        // Name of meal
        public string Name { get; set; }
        // Weight of meal in grams
        public int Weight { get; set; }
        // Number of calories per meal in kilocalories
        public int Calorie { get; set; }
        // Price of meal in Rubles
        public decimal Price { get; set; }

        // Id of meal's category
        public int MealCategoryId { get; set; }
        // Meal's category
        public MealCategory MealCategory { get; set; }
    }
}
