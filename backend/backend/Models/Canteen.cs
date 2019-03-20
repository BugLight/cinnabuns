namespace backend.Models
{
    public class Canteen
    {
        // Primary key
        public int Id { get; set; }

        // Does canteen accept cards ("Да"/"Нет")
        public bool AcceptCards { get; set; }
        // Some details of canteen (location, working time and etc.)
        public string Description { get; set; }
        // Name of canteen
        public string Name { get; set; }
    }
}
