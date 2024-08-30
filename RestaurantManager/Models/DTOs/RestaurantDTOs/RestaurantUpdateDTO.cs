using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Models.DTOs.RestaurantDTOs
{
    public class RestaurantUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; } 
        public string Description { get; set; }
    }
}
