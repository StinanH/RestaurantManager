using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Models.DTOs.MenuDTOs;
using RestaurantManager.Models.DTOs.MenuItemDTOs;
using RestaurantManager.Models.DTOs.RestaurantDTOs;
using RestaurantManager.Services;
using RestaurantManager.Services.IServices;

namespace RestaurantManager.Controllers
{
    [ApiController]
    [Route("restaurant/{restaurantId:int}/[controller]")]
    public class MenuController : Controller
    {
        private readonly IMenuServices _menuServices;

        public MenuController(IMenuServices menuServices)
        {
            _menuServices = menuServices;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateMenu(int restaurantId, MenuCreateDTO menuDTO)
        {
            await _menuServices.AddMenuAsync(menuDTO, restaurantId);

            return Ok("Menu "+menuDTO.Name+" created.");
        }

        [HttpGet]
        [Route("/all_menus")]
        public async Task<IActionResult> GetAllMenus(int restaurantId)
        {
            var menus = await _menuServices.GetAllMenusAsync(restaurantId);

            return Ok(menus);
        }

        [HttpGet]
        [Route("{menuId:int}")]
        public async Task<IActionResult> GetMenuAsync(int menuId, int restaurantId)
        {
            var menu = await _menuServices.GetMenuAsync(menuId, restaurantId);

            return Ok(menu);
        }

        [HttpPut]
        [Route("{menuId:int}")]
        public async Task<IActionResult> UpdateMenuAsync(int menuId, MenuUpdateDTO menuUpdateDTO, int restaurantId)
        {
            await _menuServices.UpdateMenuAsync(menuUpdateDTO, restaurantId);

            return Ok("Menu updated");
        }

        [HttpDelete]
        [Route("{menuId:int}")]
        public async Task<IActionResult> DeleteMenuAsync(int menuId, int restaurantId)
        {
            await _menuServices.DeleteMenuAsync(menuId, restaurantId);

            return Ok("Menu Deleted");
        }

        [HttpGet]
        [Route("{menuId:int}/{menuItemId:int}")]
        public async Task<IActionResult> GetMenuItemAsync(int restaurantId, int menuId, int menuItemId)
        {
            var menuItem = await _menuServices.GetMenuItemAsync(restaurantId, menuId, menuItemId);

            return Ok(menuItem);
        }

        [HttpPost]
        [Route("{menuId:int}/create")]
        public async Task<ActionResult> CreateMenuItem(int restaurantId, int menuId, MenuItemCreateDTO menuItemDTO)
        {
            await _menuServices.AddMenuItemAsync(restaurantId, menuId, menuItemDTO);

            return Ok("Menu Created");
        }

        [HttpPut]
        [Route("{menuId:int}/{menuItemId:int}")]
        public async Task<ActionResult> UpdateMenuItemAsync (int restaurantId, int menuId, MenuItemUpdateDTO menuItemDTO)
        {
            await _menuServices.UpdateMenuItemAsync(menuItemDTO, restaurantId);

            return Ok("menuitem updated.");
        
        }

        [HttpDelete]
        [Route("{menuId:int}/{menuItemId:int}")]
        public async Task<IActionResult> DeleteMenuItemAsync(int menuItemId, int menuId, int restaurantId)
        {
            await _menuServices.DeleteMenuItemAsync(menuItemId, restaurantId);

            return Ok("MenuItem Deleted");
        }


    }
}
