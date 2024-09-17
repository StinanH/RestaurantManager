using Microsoft.EntityFrameworkCore;
using RestaurantManager.Data.Repos.IRepos;
using RestaurantManager.Models;

namespace RestaurantManager.Data.Repos
{
    public class MenuRepository : IMenuRepository
    {
        public readonly RestaurantManagerContext _context;

        public MenuRepository(RestaurantManagerContext context)
        {
            _context = context;
        }

        //Get all Menus of restaurant 
        public async Task<IEnumerable<Menu>> GetAllMenusAsync(int restaurantId)
        {
            var menus = await _context.Menus
                .Where(m => m.FK_RestaurantId == restaurantId)
                .Include(m => m.MenuItems)
                .ToListAsync();

            return menus;
        }

        //Get restaurants menu by ID
        public async Task<Menu> GetMenuAsync(int menuId, int restaurantId)
        {
            var menu = await _context.Menus
                .Where(m => m.FK_RestaurantId == restaurantId && m.Id == menuId)
                .Include(m => m.MenuItems)
                .FirstOrDefaultAsync(m => m.Id == menuId);

            return menu;
        }

        //Get menuitems by ID
        public async Task<MenuItem> GetMenuItemAsync(int menuItemId, int restaurantId)
        {
            var menuItem = await _context.MenuItems
                .Where(m => m.FK_RestaurantId == restaurantId)
                .FirstOrDefaultAsync(m => m.Id == menuItemId);

            return menuItem;
        }

        //Add new menu
        public async Task AddMenuAsync(Menu menu, int restaurantId)
        {
            await _context.Menus.AddAsync(menu);

            Restaurant restaurantUsingMenu = await _context.Restaurants.FirstOrDefaultAsync(r => menu.FK_RestaurantId == r.Id);

            restaurantUsingMenu.Menus.Add(menu);

            await _context.SaveChangesAsync();
        }

        //Add new item to menu
        public async Task AddMenuItemAsync(MenuItem menuItem, int restaurantId)
        {
            await _context.MenuItems.AddAsync(menuItem);

            await _context.SaveChangesAsync();

            Menu MenuToAddMenuItemTo = await _context.Menus.FirstOrDefaultAsync(m => menuItem.FK_MenuId == m.Id);

            MenuToAddMenuItemTo.MenuItems.Add(menuItem);

            await _context.SaveChangesAsync();
        }

        //Update menu
        public async Task UpdateMenuAsync(Menu menu, int restaurantId)
        {
            _context.Menus.Update(menu);

            await _context.SaveChangesAsync();
        }

        //Update menuItem
        public async Task UpdateMenuItemAsync(MenuItem menuItem)
        {
            _context.MenuItems.Update(menuItem);

            await _context.SaveChangesAsync();
        }

        //Delete menu
        public async Task DeleteMenuAsync(Menu menu, int restaurantId)
        {
            /*
             * Try adding if menuitems aren't getting deleted when menu is deleted
             * 
            await _context.MenuItems
                .Where(mi => mi.MenuId == menu.Id)
                .ExecuteDeleteAsync();
            */

            _context.Menus.Remove(menu);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteMenuItemAsync(MenuItem menuItem)
        {
            _context.MenuItems.Remove(menuItem);

            await _context.SaveChangesAsync();
        }
    }
}
