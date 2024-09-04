using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManager.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Restaurant")]
        public int FK_RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        [Required]
        public string Name {  get; set; }
        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
