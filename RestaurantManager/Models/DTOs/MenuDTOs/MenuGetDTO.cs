using RestaurantManager.Models.DTOs.MenuItemDTOs;

namespace RestaurantManager.Models.DTOs.MenuDTOs
{
    public class MenuGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MenuItemGetDTO> MenuItems { get; set; } = new List<MenuItemGetDTO>();
    }
}
