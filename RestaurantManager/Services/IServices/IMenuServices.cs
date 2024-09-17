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
        Task<MenuGetDTO> GetMenuAsync(int menuId,int restaurantId);
        Task AddMenuAsync(MenuCreateDTO menuDTO, int restaurantId);
        Task UpdateMenuAsync(MenuUpdateDTO menuDTO, int restaurantId);
        Task DeleteMenuAsync(int menuId, int restaurantId);

        //Menuitems
        Task<MenuItemGetDTO> GetMenuItemAsync(int restaurantId, int menuId, int menuItemId);
        Task AddMenuItemAsync(int restaurantId, int menuId, MenuItemCreateDTO menuItemDTO);
        Task UpdateMenuItemAsync(MenuItemUpdateDTO menuItemDTO, int restaurantId);
        Task DeleteMenuItemAsync(int menuItemId, int restaurantId);
    }
}
