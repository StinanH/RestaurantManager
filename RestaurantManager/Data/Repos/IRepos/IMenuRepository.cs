using RestaurantManager.Models;

namespace RestaurantManager.Data.Repos.IRepos
{
    public interface IMenuRepository
    {
        //Menu
        Task<IEnumerable<Menu>> GetAllMenusAsync(int restaurantId);
        Task<Menu> GetMenuAsync(int menuId);
        Task AddMenuAsync(Menu menu);
        Task UpdateMenuAsync(Menu menu);
        Task DeleteMenuAsync(Menu menu);

        //Menuitems
        Task<MenuItem> GetMenuItemAsync(int menuItemId);
        Task AddMenuItemAsync(MenuItem menuItem);
        Task UpdateMenuItemAsync(MenuItem menuItem);
        Task DeleteMenuItemAsync(MenuItem menuItem);

    }
}
