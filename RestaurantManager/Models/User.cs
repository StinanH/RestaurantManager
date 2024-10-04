
using RestaurantManager.Services;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
