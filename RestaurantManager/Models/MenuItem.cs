using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManager.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("Menu")]
        public int MenuId { get; set; }

        [Required]
        public string Name {  get; set; }

        public string Category { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool isAvaliable { get; set; }

        public int AmountAvaliable { get; set; }
        
    }
}
