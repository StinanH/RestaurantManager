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
            var restaurant = await _context.Restaurants
                .Where(r => r.Id == restaurantId)
                .Include(r => r.Menus)
                .ThenInclude(r => r.MenuItems)
                .FirstOrDefaultAsync();

            return restaurant;
        }

        //Add new restaurant
        public async Task AddRestaurantAsync(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);

            await _context.SaveChangesAsync();
        }

        //Add Table to existing restaurant
        public async Task AddTableToRestaurantAsync(int restaurantId, Table table)
        {
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.Id ==restaurantId);

            restaurant.Tables.Add(table);

            await _context.SaveChangesAsync();
        }

        //Add Menu to existing restaurant
        public async Task AddBookingToRestaurantAsync(int restaurantId, Booking booking)
        {
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.Id == restaurantId);

            restaurant.Bookings.Add(booking);

            await _context.SaveChangesAsync();
        }

        //Add Menu to existing restaurant
        public async Task AddMenuToRestaurantAsync(int restaurantId, Menu menu)
        {
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.Id == restaurantId);

            restaurant.Menus.Add(menu);

            await _context.SaveChangesAsync();
        }

        //Update existing restaurant
        public async Task UpdateRestaurantAsync(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);

            await _context.SaveChangesAsync();

        }

        //Delete existing restaurant
        public async Task DeleteRestaurantAsync(Restaurant restaurant)
        {
            _context.Restaurants.Remove(restaurant);

            await _context.SaveChangesAsync();
        }
    }
}
