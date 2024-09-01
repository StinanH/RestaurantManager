using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.BookingDTOs;

namespace RestaurantManager.Services.IServices
{
    public interface IBookingServices
    {
        Task<IEnumerable<BookingGetDTO>> GetAllBookingsAsync();
        Task<IEnumerable<BookingGetDTO>> GetAllBookingsByRestaurantIdAsync(int restaurantId);
        Task<BookingGetDTO> GetBookingAsync(int bookingID);
        Task AddBookingAsync(BookingCreateDTO bookingDTO);
        Task UpdateBookingAsync(BookingUpdateDTO bookingDTO);
        Task DeleteBookingAsync(int bookingId);
    }
}
