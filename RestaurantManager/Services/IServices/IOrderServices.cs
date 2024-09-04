using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.OrderDTOs;

namespace RestaurantManager.Services.IServices
{
    public interface IOrderServices
    {
        Task<IEnumerable<OrderGetDTO>> GetAllOrdersAsync();
        Task<IEnumerable<OrderGetDTO>> GetAllOrdersAsync(int userId);
        Task<OrderGetDTO> GetOrderAsync(int orderID);
        Task AddOrderAsync(OrderCreateDTO orderDTO);
        Task UpdateOrderAsync(OrderUpdateDTO orderDTO);
        Task DeleteOrderAsync(int orderId);
    }
}
