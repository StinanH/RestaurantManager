using RestaurantManager.Models;

namespace RestaurantManager.Data.Repos.IRepos
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetAllOrdersAsync(int userId);
        Task<Order> GetOrderAsync(int orderID);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(Order order);
    }
}
