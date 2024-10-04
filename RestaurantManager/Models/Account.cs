using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManager.Models
{
    public class Account
    {
        public int Id { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHashed { get; set; }

        [Required]
        public bool isAdmin { get; set; }

        public User User { get; set; }

        [Required]
        [ForeignKey("User")]
        public int FK_User {  get; set; }
    }
}
