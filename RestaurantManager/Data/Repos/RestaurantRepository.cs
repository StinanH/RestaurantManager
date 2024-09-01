using Microsoft.EntityFrameworkCore;
using RestaurantManager.Data.Repos.IRepos;
using RestaurantManager.Models;

namespace RestaurantManager.Data.Repos
{
    public class RestaurantRepository : IRestaurantRepository
    {
        public readonly RestaurantManagerContext _context;

        public RestaurantRepository(RestaurantManagerContext context)
        {
            _context = context;
        }

        //Get all restaurants
        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
        {
            var restaurantList = await _context.Restaurants.ToListAsync();

            return restaurantList;
        }

        //Get restaurant by ID
        public async Task<Restaurant> GetRestaurantAsync(int restaurantId)
        {
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.Id == restaurantId);

            return restaurant;
        }

        //Add new restaurant
        public async Task AddRestaurantAsync(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);

            await _context.SaveChangesAsync();
        }

        //Update restaurant
        public async Task UpdatRestaurantAsync(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);

            await _context.SaveChangesAsync();

        }

        //Delete restaurant
        public async Task DeleteRestaurantAsync(Restaurant restaurant)
        {
            _context.Restaurants.Remove(restaurant);

            await _context.SaveChangesAsync();
        }
    }
}
