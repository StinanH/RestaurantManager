using RestaurantManager.Models;

namespace RestaurantManager.Data.Repos.IRepos
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
        Task<Restaurant> GetRestaurantAsync(int restaurantID);
        Task AddRestaurantAsync(Restaurant restaurant);
        Task AddTableToRestaurantAsync(int restaurantId, Table table);
        Task UpdateRestaurantAsync(Restaurant restaurant);
        Task DeleteRestaurantAsync(Restaurant restaurant);


    }
}
