namespace RestaurantManager.Models.DTOs.TimeSlotDTOs
{
    public class TimeslotCreateDTO
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool isAvaliable { get; set; }

    }
}
