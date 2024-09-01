using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.MenuDTOs;
using RestaurantManager.Models.DTOs.MenuItemDTOs;
using RestaurantManager.Models.DTOs.RestaurantDTOs;

namespace RestaurantManager.Services.IServices
{
    public interface IMenuServices
    {
        //Menu
        Task<IEnumerable<MenuGetDTO>> GetAllMenusAsync(int restaurantId);
        Task<MenuGetDTO> GetMenuAsync(int menuId);
        Task AddMenuAsync(MenuCreateDTO menuDTO);
        Task UpdateMenuAsync(MenuUpdateDTO menuDTO);
        Task DeleteMenuAsync(int menuId);

        //Menuitems
        Task<MenuItemGetDTO> GetMenuItemAsync(int menuItemId);
        Task AddMenuItemAsync(MenuItemCreateDTO menuItemDTO);
        Task UpdateMenuItemAsync(MenuItemUpdateDTO menuItemDTO);
        Task DeleteMenuItemAsync(int menuItemId);
    }
}
