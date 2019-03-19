namespace backend.Models
{
    public class MealCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CanteenId { get; set; }
        public Canteen Canteen { get; set; }
    }
}
