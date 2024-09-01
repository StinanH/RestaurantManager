using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManager.Models
{
    public class Table
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Restaurant")]
        public int FK_RestaurantId { get; set; }

        [Required]
        public int NrOfSeats {  get; set; }

    }
}
