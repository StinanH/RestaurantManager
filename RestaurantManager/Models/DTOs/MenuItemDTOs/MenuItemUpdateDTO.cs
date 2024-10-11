using Microsoft.Identity.Client;

namespace RestaurantManager.Models.DTOs.MenuItemDTOs
{
    public class MenuItemUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }

    }
}
