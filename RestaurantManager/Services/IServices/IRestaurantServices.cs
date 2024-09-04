using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.RestaurantDTOs;
using RestaurantManager.Models.DTOs.TableDTOs;

namespace RestaurantManager.Services.IServices
{
    public interface IRestaurantServices
    {
        Task<IEnumerable<RestaurantGetDTO>> GetAllRestaurantsAsync();
        Task<RestaurantGetDTO> GetRestaurantAsync(int restaurantID);
        Task AddRestaurantAsync(RestaurantCreateDTO restaurantDTO);
        Task UpdateRestaurantAsync(RestaurantUpdateDTO restaurantDTO);
        Task DeleteRestaurantAsync(int restaurantId);
    }
}
