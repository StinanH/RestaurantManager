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
            var menuList = await _context.Menus
                .Where(m => m.Id == restaurantId)
                .Include(m => m.MenuItems)
                .ToListAsync();

            return menuList;
        }

        //Get menu by ID
        public async Task<Menu> GetMenuAsync(int menuId)
        {
            var menu = await _context.Menus
                .Include(m => m.MenuItems)
                .FirstOrDefaultAsync(m => m.Id == menuId);

            return menu;
        }

        //Get menuitem by ID
        public async Task<MenuItem> GetMenuItemAsync(int menuItemId)
        {
            var menuItem = await _context.MenuItems.FirstOrDefaultAsync(m => m.Id == menuItemId);

            return menuItem;
        }

        //Add new menu
        public async Task AddMenuAsync(Menu menu)
        {
            await _context.Menus.AddAsync(menu);

            await _context.SaveChangesAsync();
        }

        //Add new item to menu
        public async Task AddMenuItemAsync(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);

            await _context.SaveChangesAsync();
        }

        //Update menu
        public async Task UpdateMenuAsync(Menu menu)
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
        public async Task DeleteMenuAsync(Menu menu)
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
