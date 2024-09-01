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
        public int RestaurantId { get; set; }

        [Required]
        public string Name {  get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }

        //menu days menu is avaliable

        //hours in case of lunchmenu

    }
}
