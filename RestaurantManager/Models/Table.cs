using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Models
{
    public class Table
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NrOfSeats {  get; set; }

    }
}
