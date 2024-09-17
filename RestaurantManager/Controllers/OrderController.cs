using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Models.DTOs.OrderDTOs;
using RestaurantManager.Services.IServices;


namespace RestaurantManager.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class OrderController : Controller
    {
        public readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }


        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateOrder(OrderCreateDTO orderDto)
        {
            await _orderServices.AddOrderAsync(orderDto);

            return Ok("Order added for " + orderDto.FK_UserId);
        }

        [HttpGet]
        [Route("/{orderId:int}")]

        public async Task<IActionResult> GetOrderAsync(int orderId)
        {
            var order = await _orderServices.GetOrderAsync(orderId);

            return Ok(order);
        }

        [HttpGet]
        [Route("/all")]
        public async Task<IActionResult> GetAllOrderAsync(int userId)
        {
            var order = await _orderServices.GetOrderAsync(userId);

            return Ok(order);
        }

        //[HttpPut]
        //[Route("/")]
        //public async Task<IActionResult> UpdateOrderAsync(OrderUpdateDTO orderDTO)
        //{
        //    await _orderServices.UpdateOrderAsync(orderDTO);

        //    return Ok("order updated");
        //}

        [HttpDelete]
        [Route("/{orderId:int}")]
        public async Task<IActionResult> DeleteOrderAsync(int orderId)
        {
            await _orderServices.DeleteOrderAsync(orderId);

            return Ok("order with id " + orderId + " deleted.");
        }


    }
}
