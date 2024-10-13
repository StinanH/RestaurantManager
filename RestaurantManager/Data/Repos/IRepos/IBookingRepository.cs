using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.BookingDTOs;

namespace RestaurantManager.Data.Repos.IRepos
{
    public interface IBookingRepository
    {
        Task<IEnumerable<BookingCreateDTO>> GetAvaliableBookingsOnDayAsync(int restaurantId, int numberOfPeople, DateTime date);
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<IEnumerable<Booking>> GetAllBookingsByRestaurantIdAsync(int restaurantId);
        Task<Booking> GetBookingAsync(int bookingID);
        Task AddBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(Booking booking);
        Task<IEnumerable<Table>> IsBookingAvaliable(Booking booking);

    }
}
