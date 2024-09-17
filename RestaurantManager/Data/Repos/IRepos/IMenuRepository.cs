using RestaurantManager.Models;

namespace RestaurantManager.Data.Repos.IRepos
{
    public interface IMenuRepository
    {
        //Menu
        Task<IEnumerable<Menu>> GetAllMenusAsync(int restaurantId);
        Task<Menu> GetMenuAsync(int menuId, int restaurantId);
        Task AddMenuAsync(Menu menu, int restaurantId);
        Task UpdateMenuAsync(Menu menu, int restaurantId);
        Task DeleteMenuAsync(Menu menu, int restaurantId);

        //Menuitems
        Task<MenuItem> GetMenuItemAsync(int menuItemId, int restaurantId);
        Task AddMenuItemAsync(MenuItem menuItem, int restaurantId);
        Task UpdateMenuItemAsync(MenuItem menuItem);
        Task DeleteMenuItemAsync(MenuItem menuItem);

    }
}
