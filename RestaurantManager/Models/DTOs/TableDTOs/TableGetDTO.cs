using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Models.DTOs.TableDTOs
{
    public class TableGetDTO
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int NrOfSeats { get; set; }
    }
}
