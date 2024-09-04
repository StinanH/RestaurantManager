using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Models.DTOs.MenuDTOs;
using RestaurantManager.Models.DTOs.MenuItemDTOs;
using RestaurantManager.Models.DTOs.RestaurantDTOs;
using RestaurantManager.Services;
using RestaurantManager.Services.IServices;

namespace RestaurantManager.Controllers
{
    [ApiController]
    [Route("restaurant:{restaurantId:int}/[controller]")]
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
            await _menuServices.AddMenuAsync(restaurantId, menuDTO);

            return Ok();
        }

        [HttpGet]
        [Route("all_menus")]
        public async Task<IActionResult> GetAllMenus(int restaurantID)
        {
            var users = await _menuServices.GetAllMenusAsync(restaurantID);
            return View(users);
        }

        [HttpGet]
        [Route("{menuId:int}")]
        public async Task<IActionResult> GetMenuAsync(int menuId)
        {
            await _menuServices.GetMenuAsync(menuId);

            return Ok();
        }

        [HttpPut]
        [Route("update/{menuId:int}")]
        public async Task<IActionResult> UpdateMenuAsync(int menuId, MenuUpdateDTO menuUpdateDTO)
        {
            await _menuServices.UpdateMenuAsync(menuUpdateDTO);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{menuId:int}")]
        public async Task<IActionResult> DeleteMenuAsync(int menuId)
        {
            await _menuServices.DeleteMenuAsync(menuId);

            return Ok();
        }

        //Should take in restaurantID and menuId
        [HttpGet]
        [Route(":{menuId:int}/menuitem:{menuItemId:int}")]
        public async Task<IActionResult> GetMenuItemAsync(int restaurantId, int menuId, int menuItemId)
        {
            await _menuServices.GetMenuItemAsync(restaurantId, menuId, menuItemId);

            return Ok();
        }

        [HttpPost]
        [Route(":{menuId:int}/create")]
        public async Task<ActionResult> CreateMenuItem(int restaurantId, int menuId, MenuItemCreateDTO menuItemDTO)
        {
            await _menuServices.AddMenuItemAsync(restaurantId, menuId, menuItemDTO);

            return Ok();
        }


    }
}
