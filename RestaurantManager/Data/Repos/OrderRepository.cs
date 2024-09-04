using Microsoft.EntityFrameworkCore;
using RestaurantManager.Models;
using RestaurantManager.Data.Repos.IRepos;

namespace RestaurantManager.Data.Repos
{
    public class OrderRepository : IOrderRepository
    {
        public readonly RestaurantManagerContext _context;

        public OrderRepository(RestaurantManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orderList = await _context.Orders.ToListAsync();

            return orderList;
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync(int userId)
        {
            var orderList = await _context.Orders
                .Where(o => o.FK_UserId == userId)
                .ToListAsync();

            return orderList;
        }
        public async Task<Order> GetOrderAsync(int orderID)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderID);

            return order;
        }

        //make some checks if valid
        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);

            await _context.SaveChangesAsync();

            User userMakingOrder = await _context.Users.FirstOrDefaultAsync(u => order.FK_UserId == u.Id);

            order.User = userMakingOrder;

            userMakingOrder.Orders.Add(order);

            Restaurant restaurantTakingOrder = await _context.Restaurants.FirstOrDefaultAsync(r => order.FK_RestaurantID == r.Id);

            order.Restaurant = restaurantTakingOrder;

            restaurantTakingOrder.CurrentOrders.Add(order);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);

            await _context.SaveChangesAsync();
        }
        public async Task DeleteOrderAsync(Order order)
        {
            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();
        }
    }
}
