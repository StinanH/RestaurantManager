using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Models.DTOs.RestaurantDTOs
{
    public class RestaurantCreateDTO
    {
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
