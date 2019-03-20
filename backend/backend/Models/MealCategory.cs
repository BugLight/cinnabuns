namespace backend.Models
{
    public class MealCategory
    {
        // Primary key
        public int Id { get; set; }

        // Name of category
        public string Name { get; set; }

        // Id of category's canteen
        public int CanteenId { get; set; }
        // Category's canteen
        public Canteen Canteen { get; set; }
    }
}
