using RestaurantManager.Models.DTOs.MenuItemDTOs;

namespace RestaurantManager.Models.DTOs.OrderDTOs
{
    public class OrderCreateDTO
    {
        public int FK_UserId { get; set; }
        public int FK_RestaurantId { get; set; }
        public ICollection<MenuItemGetDTO> itemsInOrder { get; set; }
    }
}

