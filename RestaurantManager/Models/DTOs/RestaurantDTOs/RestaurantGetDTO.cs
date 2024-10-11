using RestaurantManager.Models.DTOs.MenuDTOs;

namespace RestaurantManager.Models.DTOs.RestaurantDTOs
{
    public class RestaurantGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public ICollection<MenuGetDTO> Menus { get; set; } = new List<MenuGetDTO>();
    }
}
