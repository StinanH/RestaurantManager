using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.RestaurantDTOs;

namespace RestaurantManager.Services.IServices
{
    public interface IRestaurantServices
    {
        Task<IEnumerable<RestaurantGetDTO>> GetAllRestaurantsAsync();
        Task<RestaurantGetDTO> GetRestaurantAsync(int restaurantID);
        Task AddRestaurantAsync(RestaurantCreateDTO restaurantDTO);
        Task UpdatRestaurantAsync(RestaurantUpdateDTO restaurantDTO);
        Task DeleteRestaurantAsync(int restaurantId);
    }
}
