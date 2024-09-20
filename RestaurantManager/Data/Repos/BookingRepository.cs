using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RestaurantManager.Data.Repos.IRepos;
using RestaurantManager.Models;
using System.Runtime.InteropServices;

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
                .Include(b => b.Timeslot)
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
                .Include(b => b.Timeslot)
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

        //should return a list of avaliable tables.
        public async Task<IEnumerable<Table>> IsBookingAvaliable(Booking booking)
        {
            var BookingsAtThatTimeList = await _context.Bookings
                .Where(b => b.Restaurant.Id == booking.FK_RestaurantId && b.Timeslot.StartTime <= booking.requestedTime && b.Timeslot.EndTime >= booking.requestedEndTime)
                .Include(b => b.Table)
                .ToListAsync();

            var TablesAtRestaurant = await _context.Tables
                .Where(t => t.FK_RestaurantId == booking.FK_RestaurantId)
                .OrderByDescending(t => t.NrOfSeats)
                .ToListAsync();

            if (BookingsAtThatTimeList.Count >= TablesAtRestaurant.Count)
            {
                //all tables are booked
                return new List<Table>();
            }


            else
            {
                //get ids of tables booked at that time.
                var bookedTableIds = BookingsAtThatTimeList.Select(b => b.FK_TableId).Distinct().ToList() ?? new List<int>();

                //save avaliable tables to a list
                var avaliableTables = TablesAtRestaurant.Where(t => !bookedTableIds.Contains(t.Id)).ToList();

                return avaliableTables;
            }
        }

        //make task a bool
        public async Task AddBookingAsync(Booking booking)
        {
            var avaliableTables = await IsBookingAvaliable(booking);

            if (avaliableTables.Count() == 0)
            {
                Console.WriteLine("booking failed, no avaliable tables");

                //return false;
            }

            else
            {
                //create booking
                await _context.Bookings.AddAsync(booking);

                await _context.SaveChangesAsync();

                var restaurant = await _context.Restaurants
                    .FirstOrDefaultAsync(r => booking.FK_RestaurantId == r.Id);

                if (restaurant == null)
                {
                    //return false?? no restaurant with that id found
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => booking.FK_UserID == u.Id);

                if (user == null)
                {
                    //return false?? no user found with that id.
                }

                restaurant.Bookings.Add(booking);

                user.Bookings.Add(booking);

                await _context.SaveChangesAsync();

            }
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
