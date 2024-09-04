using RestaurantManager.Models.DTOs.TableDTOs;
using RestaurantManager.Models.DTOs.TimeSlotDTOs;

namespace RestaurantManager.Models.DTOs.BookingDTOs
{
    public class BookingCreateDTO
    {
        public int RestaurantId { get; set; }
        public DateTime requestedTime { get; set; }
        public int UserId {  get; set; }
        public int NrOfPeople { get; set; }
        public string Requests { get; set; }
    }
}
