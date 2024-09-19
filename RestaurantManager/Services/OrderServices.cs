using RestaurantManager.Data.Repos.IRepos;
using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.MenuItemDTOs;
using RestaurantManager.Models.DTOs.OrderDTOs;
using RestaurantManager.Services.IServices;

namespace RestaurantManager.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMenuRepository _menuRepository;

        public OrderServices(IOrderRepository orderRepository, IMenuRepository menuRepository)
        {
            _orderRepository = orderRepository;
            _menuRepository = menuRepository;
        }

        public async Task<IEnumerable<OrderGetDTO>> GetAllOrdersAsync()
        {
            var allOrders = await _orderRepository.GetAllOrdersAsync();

            var orderList = allOrders.Select(o => new OrderGetDTO
            {
                Id = o.Id,
                FK_RestaurantId = o.FK_RestaurantID,
                FK_UserId = o.FK_UserId

                //Add that it prints items in order.
                
            });

            return orderList;

        }
        public async Task<IEnumerable<OrderGetDTO>> GetAllOrdersAsync(int userId)
        {
            var allOrders = await _orderRepository.GetAllOrdersAsync();

            var orderList = allOrders.Select(o => new OrderGetDTO
            {
                Id = o.Id,
                FK_RestaurantId = o.FK_RestaurantID,
                FK_UserId = o.FK_UserId,
                MenuItems = o.MenuItems.Select(o => new MenuItemGetDTO
                {
                    Id = o.Id,
                    Name = o.Name,
                    Category = o.Category,
                    Description = o.Description,
                    FK_MenuId = o.FK_MenuId,
                    FK_RestaurantId = o.FK_RestaurantId,
                    IsAvaliable = o.isAvaliable,
                    AmountAvaliable = o.AmountAvaliable
                }).ToList()

                //Add that it prints items in order.

            });

            return orderList;

        }
        public async Task<OrderGetDTO> GetOrderAsync(int orderID)
        {
            var orderById = await _orderRepository.GetOrderAsync(orderID);

            var order = new OrderGetDTO
            {
                Id = orderById.Id,
                FK_RestaurantId = orderById.FK_RestaurantID,
                FK_UserId = orderById.FK_UserId
            };

            foreach (MenuItem m in orderById.MenuItems)
            {
                order.MenuItems.Add(new MenuItemGetDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    Category=m.Category,
                    Description = m.Description,
                    FK_MenuId = m.FK_MenuId,
                    FK_RestaurantId = m.FK_RestaurantId,
                    AmountAvaliable = m.AmountAvaliable,
                    IsAvaliable = m.isAvaliable
                });
            }

            return order;

        }
        public async Task AddOrderAsync(OrderCreateDTO orderDTO)
        {
            var orderToAdd = new Order
            {
                FK_UserId = orderDTO.FK_UserId,
                FK_RestaurantID = orderDTO.FK_RestaurantId
            };

            foreach (MenuItemGetDTO mi in orderDTO.itemsInOrder)
            {

                var miToAdd = await _menuRepository.GetMenuItemAsync(mi.Id, orderDTO.FK_RestaurantId);

                orderToAdd.MenuItems.Add(miToAdd);
            }

            //Add new order
            await _orderRepository.AddOrderAsync(orderToAdd);

        }
        public async Task UpdateOrderAsync(OrderUpdateDTO orderDTO)
        {
            var orderToUpdate = await _orderRepository.GetOrderAsync(orderDTO.Id);

            //clear list of menuitems
            orderToUpdate.MenuItems.Clear();

            //add menuitems from orderdto to ordertoupdate.
            foreach (MenuItemGetDTO mi in orderDTO.itemsInOrder)
            {
                var miToAdd = await _menuRepository.GetMenuItemAsync(mi.Id, orderDTO.FK_RestaurantId);

                orderToUpdate.MenuItems.Add(miToAdd);
            }

            //UpdateOrder
            await _orderRepository.UpdateOrderAsync(orderToUpdate);
        }
        public async Task DeleteOrderAsync(int orderId)
        {
            var orderToDelete = await _orderRepository.GetOrderAsync(orderId);

            await _orderRepository.DeleteOrderAsync(orderToDelete);
        }

    }
}
