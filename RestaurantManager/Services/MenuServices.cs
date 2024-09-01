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
                Name = m.Name
            }).ToList();

            return menuList;
        }
        public async Task<MenuGetDTO> GetMenuAsync(int menuId)
        {
            Menu menuById = await _menuRepository.GetMenuAsync(menuId);

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
                }).ToList()
            };

            return menu;

        }

        //return bool on these 3 to report success?
        public async Task AddMenuAsync(MenuCreateDTO menuDTO)
        {
            Menu menuToAdd = new Menu
            {
                Name = menuDTO.Name,
                RestaurantId = menuDTO.RestaurantId,
            };

            await _menuRepository.AddMenuAsync(menuToAdd);

        }
        public async Task UpdateMenuAsync(MenuUpdateDTO menuDTO)
        {
            //check if any of the required fields are empty (DTO.name), if so return false.
            Menu menuToUpdate = await _menuRepository.GetMenuAsync(menuDTO.Id);

            //check if ToUpdate result found == null, if so return false

            menuToUpdate.Name = menuDTO.Name;
            
            await _menuRepository.UpdateMenuAsync(menuToUpdate);

        }
        public async Task DeleteMenuAsync(int menuId)
        {
            //check if any of the required fields are empty (DTO.name), if so return false.
            Menu menuToDelete = await _menuRepository.GetMenuAsync(menuId);

            //check if ToUpdate result found == null, if so return false

            await _menuRepository.DeleteMenuAsync(menuToDelete); 
        }

        //MenuItems
        public async Task<MenuItemGetDTO> GetMenuItemAsync(int menuItemId)
        {
            MenuItem menuItemById = await _menuRepository.GetMenuItemAsync(menuItemId);

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

        //return bool on these 3 to report success?
        public async Task AddMenuItemAsync(MenuItemCreateDTO menuItem)
        {
            MenuItem menuItemToAdd = new MenuItem
            {
                Name = menuItem.Name,
                Description = menuItem.Description,
                Category = menuItem.Category,
                AmountAvaliable = menuItem.AmountAvaliable
            };

            await _menuRepository.AddMenuItemAsync(menuItemToAdd);
        }
        public async Task UpdateMenuItemAsync(MenuItemUpdateDTO menuItem)
        {
            //check if any of the required fields are empty (DTO.name), if so return false.
            var menuItemToUpdate = await _menuRepository.GetMenuItemAsync(menuItem.Id);

            //check if ToUpdate result found == null, if so return false
            menuItemToUpdate.Name = menuItem.Name;
            menuItemToUpdate.Description = menuItem.Description;
            menuItemToUpdate.Category = menuItem.Category;
            menuItemToUpdate.AmountAvaliable = menuItem.AmountAvaliable;
        
            await _menuRepository.UpdateMenuItemAsync(menuItemToUpdate);
        }
        public async Task DeleteMenuItemAsync(int menuItemId)
        {
            MenuItem menuItemById = await _menuRepository.GetMenuItemAsync(menuItemId);

            await _menuRepository.DeleteMenuItemAsync(menuItemById);
        }
    }
}
