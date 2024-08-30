using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public string Name {  get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }

        //menu days menu is avaliable

        //hours in case of lunchmenu

    }
}
