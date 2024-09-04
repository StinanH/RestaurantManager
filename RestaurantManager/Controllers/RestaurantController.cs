using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Models.DTOs.RestaurantDTOs;
using RestaurantManager.Models.DTOs.UserDTOs;
using RestaurantManager.Services;
using RestaurantManager.Services.IServices;

namespace RestaurantManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantServices _restaurantServices;

        public RestaurantController(IRestaurantServices restaurantServices)
        {
            _restaurantServices = restaurantServices;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateRestaurant(RestaurantCreateDTO restaurantDTO)
        {
            await _restaurantServices.AddRestaurantAsync(restaurantDTO);

            return Ok("Restaurant : "+restaurantDTO.Name +" created.");
        }

        [HttpGet]
        [Route("all_restaurants")]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await _restaurantServices.GetAllRestaurantsAsync();
            return Ok(restaurants);
        }

        [HttpGet]
        [Route("{restaurantId:int}")]
        public async Task<IActionResult> GetRestaurant(int restaurantId)
        {
            var restaurant = await _restaurantServices.GetRestaurantAsync(restaurantId);

            return Ok(restaurant);
        }

        [HttpPut]
        [Route("{restaurantId:int}")]
        public async Task<IActionResult> UpdateRestaurant(int restaurantId, RestaurantUpdateDTO restaurantDTO)
        {
            await _restaurantServices.UpdateRestaurantAsync(restaurantDTO);

            return Ok("Restaurantinformation updated.");
        }

        [HttpDelete]
        [Route("{restaurantId:int}")]
        public async Task<IActionResult> DeleteRestaurant(int restaurantId)
        {
            await _restaurantServices.DeleteRestaurantAsync(restaurantId);

            return Ok("Restaurant deleted.");
        }
    }
}
