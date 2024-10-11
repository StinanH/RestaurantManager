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
        public int FK_MenuId { get; set; }

        [Required]
        [ForeignKey("Restaurant")]
        public int FK_RestaurantId { get; set; }

        public Menu Menu { get; set; }

        [Required]
        public string Name {  get; set; }

        public string Category { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        public int Price { get; set; }

        public int AmountSold { get; set; }
        
    }
}
