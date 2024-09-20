using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Models.DTOs.BookingDTOs;
using RestaurantManager.Services.IServices;

namespace RestaurantManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : Controller
    {
        private readonly IBookingServices _bookingServices;

        public BookingController(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateBooking(BookingCreateDTO bookingDTO)
        {
            var successful = await _bookingServices.AddBookingAsync(bookingDTO);

            if (successful)
            {
                return Ok("Booking created");
            }
            else
            {
                return NotFound("Booking unsuccessful, no avaliable tables at that time");
            }
        }

        [HttpGet]
        [Route("all_bookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _bookingServices.GetAllBookingsAsync();
            return Ok(bookings);
        }

        //[HttpGet]
        //[Route("view/bookingAvaliability")]
        //public async Task<IActionResult> CheckBookingAvaliability(BookingCreateDTO bookingDTO)
        //{
        //    var isAvaliable = await _bookingServices.IsBookingAvaliable(bookingDTO);

        //    return Ok("is avaliable is : "+isAvaliable);
        //}

        [HttpGet]
        [Route("{restaurantId:int}/all_bookings")]
        public async Task<IActionResult> GetAllBookingAtRestaurant(int restaurantId)
        {
            var bookingslist = await _bookingServices.GetAllBookingsByRestaurantIdAsync(restaurantId);

            return Ok(bookingslist);
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public async Task<IActionResult> GetBooking(int bookingId)
        {
            var booking = await _bookingServices.GetBookingAsync(bookingId);

            if (booking == null)
            {
                return NotFound("Booking not found");
            }

            return Ok(booking);
        }

        [HttpPut]
        [Route("{bookingId:int}")]
        public async Task<IActionResult> UpdateBooking(int bookingId, BookingUpdateDTO bookingDTO)
        {
            await _bookingServices.UpdateBookingAsync(bookingDTO);

            return Ok("booking id :" +bookingId +"update");
        }

        [HttpDelete]
        [Route("{bookingId:int}")]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            await _bookingServices.DeleteBookingAsync(bookingId);          

            return Ok("Booking with id:" +bookingId +" deleted.");
        }
    }
}
