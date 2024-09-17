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
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        //Add hours open?!

        public ICollection<Menu> Menus { get; set; } = new List<Menu>();

        public ICollection<Table> Tables { get; set; } = new List<Table>();

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public ICollection<Order> CurrentOrders { get; set; } = new List<Order>();

    }
}
