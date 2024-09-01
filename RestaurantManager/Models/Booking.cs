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

        [MaxLength(500)]
        public string Requests {  get; set; }

        public DateTime ReservationDateTimeStart { get; set; }

        public DateTime ReservationDateTimeEnd { get; set; }

        public DateTime BookingLastUpdatedAt { get; set; }

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
