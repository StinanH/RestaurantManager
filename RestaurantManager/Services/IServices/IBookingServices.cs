using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.BookingDTOs;
using RestaurantManager.Models.DTOs.TableDTOs;

namespace RestaurantManager.Services.IServices
{
    public interface IBookingServices
    {
        Task<IEnumerable<BookingGetDTO>> GetAllBookingsAsync();
        Task<IEnumerable<BookingGetDTO>> GetAllBookingsByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<BookingCreateDTO>> GetAvaliableBookingsOnDayAsync(int restaurantId, int numberOfPeople, DateTime date);
        Task<BookingGetDTO> GetBookingAsync(int bookingID);
        Task <bool>AddBookingAsync(BookingCreateDTO bookingDTO);
        Task UpdateBookingAsync(BookingUpdateDTO bookingDTO);
        Task DeleteBookingAsync(int bookingId);
        Task<TableGetDTO> IsBookingAvaliable(BookingCreateDTO bookingDTO);
    }
}
