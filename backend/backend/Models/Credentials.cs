using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Credentials
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string PasswordHash { get; set; }

        public User User { get; set; }
    }
}
