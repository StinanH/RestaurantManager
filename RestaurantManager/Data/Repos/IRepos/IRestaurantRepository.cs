using RestaurantManager.Models;

namespace RestaurantManager.Data.Repos.IRepos
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
        Task<Restaurant> GetRestaurantAsync(int restaurantID);
        Task AddRestaurantAsync(Restaurant restaurant);
        Task UpdatRestaurantAsync(Restaurant restaurant);
        Task DeleteRestaurantAsync(Restaurant restaurant);
    }
}
