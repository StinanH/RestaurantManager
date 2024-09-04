﻿using Microsoft.AspNetCore.Mvc;
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
            await _bookingServices.AddBookingAsync(bookingDTO);

            return Ok();
        }

        [HttpGet]
        [Route("view/all_bookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _bookingServices.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet]
        [Route("view/bookingAvaliability")]
        public async Task<IActionResult> CheckBookingAvaliability(BookingCreateDTO bookingDTO)
        {
            var isAvaliable = await _bookingServices.IsBookingAvaliable(bookingDTO);

            return Ok("is avaliable is : "+isAvaliable);
        }

        [HttpGet]
        [Route("view/restaurant_bookings/{restaurantId}")]
        public async Task<IActionResult> GetAllBookingAtRestaurant(int restaurantId)
        {
            await _bookingServices.GetAllBookingsByRestaurantIdAsync(restaurantId);

            return Ok();
        }

        [HttpGet]
        [Route("view/{userId}")]
        public async Task<IActionResult> GetBooking(int bookingId)
        {
            await _bookingServices.GetBookingAsync(bookingId);

            return Ok();
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateBooking(int bookingId, BookingUpdateDTO userDTO)
        {
            await _bookingServices.UpdateBookingAsync(userDTO);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            await _bookingServices.DeleteBookingAsync(bookingId);

            return Ok();
        }
    }
}