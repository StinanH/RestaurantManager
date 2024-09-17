using RestaurantManager.Data.Repos.IRepos;
using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.MenuDTOs;
using RestaurantManager.Models.DTOs.MenuItemDTOs;
using RestaurantManager.Services.IServices;

namespace RestaurantManager.Services
{
    public class MenuServices : IMenuServices
    {
        private readonly IMenuRepository _menuRepository;

        public MenuServices(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        //Menu
        public async Task<IEnumerable<MenuGetDTO>> GetAllMenusAsync(int restaurantId)
        {
            var allMenus = await _menuRepository.GetAllMenusAsync(restaurantId);

            var menuList = allMenus.Select(m => new MenuGetDTO
            {
                Id = m.Id,
                Name = m.Name,
                MenuItems = m.MenuItems.Select(m => new MenuItemGetDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    Category = m.Category,
                    Description = m.Description
                }).ToList()
            }).ToList();


            foreach (var menu in menuList) {

                foreach (var item in menu.MenuItems) { Console.WriteLine(item.Name); }
            }

            return menuList;
        }

        public async Task<MenuGetDTO> GetMenuAsync(int menuId, int restaurantId)
        {
            Menu menuById = await _menuRepository.GetMenuAsync(menuId, restaurantId);

            var menu = new MenuGetDTO
            {
                Id = menuById.Id,
                Name = menuById.Name,
                MenuItems = menuById.MenuItems.Select(m => new MenuItemGetDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    Category = m.Category,
                    Description = m.Description,
                    AmountAvaliable = m.AmountAvaliable
                }).ToList() ?? new List<MenuItemGetDTO>()
            } ?? new MenuGetDTO { };

            return menu;

        }

        //return bool on these 3 to report success?
        public async Task AddMenuAsync(MenuCreateDTO menuDTO, int restaurantId)
        {
            Menu menuToAdd = new Menu
            {
                Name = menuDTO.Name,
                FK_RestaurantId = menuDTO.RestaurantId,
            };

            await _menuRepository.AddMenuAsync(menuToAdd, restaurantId);

        }
        public async Task UpdateMenuAsync(MenuUpdateDTO menuDTO, int restaurantId)
        {
            //check if any of the required fields are empty (DTO.name), if so return false.
            Menu menuToUpdate = await _menuRepository.GetMenuAsync(menuDTO.Id, restaurantId);

            //check if ToUpdate result found == null, if so return false

            menuToUpdate.Name = menuDTO.Name;
            
            await _menuRepository.UpdateMenuAsync(menuToUpdate, restaurantId);

        }
        public async Task DeleteMenuAsync(int menuId, int restaurantId)
        {
            //check if any of the required fields are empty (DTO.name), if so return false.
            Menu menuToDelete = await _menuRepository.GetMenuAsync(menuId, restaurantId);

            //check if ToUpdate result found == null, if so return false

            await _menuRepository.DeleteMenuAsync(menuToDelete, restaurantId); 
        }

        //MenuItems
        public async Task<MenuItemGetDTO> GetMenuItemAsync(int restaurantId, int menuId, int menuItemId)
        {
            //restauranid to check permissions

            //check that menu belongs to restaurant, and item belongs to menu, before printing item

            MenuItem menuItemById = await _menuRepository.GetMenuItemAsync(menuItemId, restaurantId);

            var menuItem = new MenuItemGetDTO
            {
                Id = menuItemById.Id,
                Name = menuItemById.Name,
                Category = menuItemById.Category,
                Description = menuItemById.Description,
                AmountAvaliable = menuItemById.AmountAvaliable
            };

            return menuItem;
        }

        //return bool on these 4 to report success?

        //add menuitem to menu

        public async Task AddMenuItemAsync(int restaurantId, int menuId, MenuItemCreateDTO menuItem)
        {
            //restaurantId so you can check it's your restaruant.

            //check that menu belongs to restaurant,save reference to menu so you can add item to its collection of menuitems.

            MenuItem menuItemToAdd = new MenuItem
            {
                Name = menuItem.Name,
                Description = menuItem.Description,
                Category = menuItem.Category,
                AmountAvaliable = menuItem.AmountAvaliable
            };

            await _menuRepository.AddMenuItemAsync(menuItemToAdd, restaurantId);
        }
        public async Task UpdateMenuItemAsync(MenuItemUpdateDTO menuItem, int restaurantId)
        {
            //check if any of the required fields are empty (DTO.name), if so return false.
            var menuItemToUpdate = await _menuRepository.GetMenuItemAsync(menuItem.Id, restaurantId);

            //check if ToUpdate result found == null, if so return false
            menuItemToUpdate.Name = menuItem.Name;
            menuItemToUpdate.Description = menuItem.Description;
            menuItemToUpdate.Category = menuItem.Category;
            menuItemToUpdate.AmountAvaliable = menuItem.AmountAvaliable;
        
            await _menuRepository.UpdateMenuItemAsync(menuItemToUpdate);
        }
        public async Task DeleteMenuItemAsync(int menuItemId, int restaurantId)
        {
            MenuItem menuItemById = await _menuRepository.GetMenuItemAsync(menuItemId, restaurantId);

            await _menuRepository.DeleteMenuItemAsync(menuItemById);
        }
    }
}
