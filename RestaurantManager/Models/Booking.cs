using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManager.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NrOfPeople { get; set; }

        [Required]
        public DateTime requestedTime { get; set; }

        public DateTime requestedEndTime { get; set; }

        [MaxLength(500)]
        public string Requests {  get; set; }

        [ForeignKey("Timeslot")]
        public int FK_TimeslotId { get; set; }

        public TimeSlot Timeslot;

        [ForeignKey("User")]
        public int FK_UserID { get; set; }
        public User User { get; set; }

        [ForeignKey("Table")]
        public int FK_TableId {  get; set; }
        public Table Table { get; set; }

        [ForeignKey("Restaurant")]
        public int FK_RestaurantId { get; set; }
        public  Restaurant Restaurant { get; set; }

    }
}
