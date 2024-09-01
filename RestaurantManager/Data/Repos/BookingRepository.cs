using Microsoft.EntityFrameworkCore;
using RestaurantManager.Data.Repos.IRepos;
using RestaurantManager.Models;

namespace RestaurantManager.Data.Repos
{
    public class BookingRepository : IBookingRepository
    {
        public readonly RestaurantManagerContext _context;

        public BookingRepository(RestaurantManagerContext context) {

            _context = context;
        }

        //Get all bookings in system
        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            var bookingsList = await _context.Bookings
                .Include(b => b.Restaurant)
                .Include(b => b.User)
                .Include(b => b.Table)
                .ToListAsync();

            return bookingsList;
        }

        //Get all bookings of restaurant in system
        public async Task<IEnumerable<Booking>> GetAllBookingsByRestaurantIdAsync(int restaurantId)
        {
            var bookingsList = await _context.Bookings
                .Where(b => b.FK_RestaurantId == restaurantId)
                .Include(b => b.Restaurant)
                .Include(b => b.User)
                .Include(b => b.Table)
                .ToListAsync();

            return bookingsList;
        }


        public async Task<Booking> GetBookingAsync(int bookingID)
        {
            var booking = await _context.Bookings
                .Include(b => b.Restaurant)
                .Include(b => b.User)
                .Include(b => b.Table)
                .FirstOrDefaultAsync(b => b.Id == bookingID);

            return booking;
        }
        public async Task AddBookingAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);

            await _context.SaveChangesAsync();

        }
        public async Task DeleteBookingAsync(Booking booking)
        {
            _context.Bookings.Remove(booking);

            await _context.SaveChangesAsync();
        }
    }
}
