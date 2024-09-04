namespace RestaurantManager.Models.DTOs.TimeSlotDTOs
{
    public class TimeslotUpdateDTO
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool isAvaliable { get; set; }

    }
}
