namespace RestaurantManager.Models.DTOs.BookingDTOs
{
    public class BookingCreateDTO
    {
        public int RestaurantId { get; set; }
        public int TableId { get; set; }
        public int UserId {  get; set; }
        public int NrOfPeople { get; set; }
        public string Requests { get; set; }
        public DateTime ReservationTime { get; set; }
    }
}
