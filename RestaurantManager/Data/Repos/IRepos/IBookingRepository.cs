using RestaurantManager.Models;

namespace RestaurantManager.Data.Repos.IRepos
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<IEnumerable<Booking>> GetAllBookingsByRestaurantIdAsync(int restaurantId);
        Task<Booking> GetBookingAsync(int bookingID);
        Task AddBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(Booking booking);
    }
}
