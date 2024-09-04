using RestaurantManager.Models.DTOs.RestaurantDTOs;
using RestaurantManager.Models.DTOs.UserDTOs;
using RestaurantManager.Models.DTOs.TimeSlotDTOs;

namespace RestaurantManager.Models.DTOs.BookingDTOs
{
    public class BookingGetDTO
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public RestaurantGetDTO Restaurant { get; set; }
        public int TableId { get; set; }
        public int UserId { get; set; }
        public UserGetDTO User { get; set; }
        public int NrOfPeople { get; set; }
        public string Requests { get; set; }
        public int TimeslotId { get; set; }
        public TimeslotGetDTO Timeslot { get; set; }
    }
}
