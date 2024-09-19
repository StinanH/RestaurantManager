using RestaurantManager.Models.DTOs.MenuItemDTOs;

namespace RestaurantManager.Models.DTOs.OrderDTOs
{
    public class OrderGetDTO
    {
        public int Id { get; set; }
        public int FK_UserId { get; set; }
        public int FK_RestaurantId { get; set; }
        public ICollection<MenuItemGetDTO> MenuItems { get; set; } = new List<MenuItemGetDTO>();
    }
}

