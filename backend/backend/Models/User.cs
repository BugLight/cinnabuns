namespace backend.Models
{
    public class User
    {
        // Primary key
        public int Id { get; set; }

        // Name of user
        public string Name { get; set; }

        // Object containg password
        public Credentials Credentials { get; set; }
    }
}
