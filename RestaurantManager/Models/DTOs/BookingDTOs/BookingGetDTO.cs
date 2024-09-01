using RestaurantManager.Models.DTOs.RestaurantDTOs;
using RestaurantManager.Models.DTOs.UserDTOs;

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
        public DateTime ReservationDateTimeStart { get; set; }

        public DateTime ReservationDateTimeEnd { get; set; }

        public DateTime BookingLastUpdatedAt { get; set; }
    }
}
