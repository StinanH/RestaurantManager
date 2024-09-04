using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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

        public async Task<bool> IsBookingAvaliable(Booking booking)
        {
            var BookingsAtThatTimeList = await _context.Bookings
                .Where(b => b.Restaurant.Id == booking.FK_RestaurantId && b.Timeslot.StartTime <= booking.requestedTime && b.Timeslot.EndTime >= booking.requestedEndTime)
                .ToListAsync();

            var TablesAtRestaurant = await _context.Tables.Where(t => t.FK_RestaurantId == booking.FK_RestaurantId).ToListAsync();

            if (BookingsAtThatTimeList.Count >= TablesAtRestaurant.Count)
            {
                //all tables are booked
                return false;
            }

            else
            {
                return true;
            }
        }

        public async Task AddBookingAsync(Booking booking)
        {
            //

            //create booking
            await _context.Bookings.AddAsync(booking);

            await _context.SaveChangesAsync();

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => booking.FK_RestaurantId == r.Id);

            var user = await _context.Users.FirstOrDefaultAsync(u => booking.FK_UserID == u.Id);


            //foreach (Booking b in restaurant.Bookings)
            //{
            //    foreach (Table t in restaurant.Tables) 
            //    { 
            //        if (b.Timeslot.StartTime <= booking.requestedTime && b.Timeslot.EndTime >= booking.requestedEndTime && booking.FK_TableId == t.Id)
            //        {

            //        } 
            //    }
            //}


            restaurant.Bookings.Add(booking);

            user.Bookings.Add(booking);

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
