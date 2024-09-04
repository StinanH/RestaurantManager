using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManager.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int FK_UserId { get; set; }
        public User User { get; set; }

        [Required]
        [ForeignKey("Restaurant")]
        public int FK_RestaurantID {  get; set; }
        public Restaurant Restaurant { get; set; }
        public ICollection<MenuItem> itemsInOrder { get; set; } = new List<MenuItem>();
    }
}
