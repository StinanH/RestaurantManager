using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public Menu Menus { get; set; }

        public ICollection<Table> Tables { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
