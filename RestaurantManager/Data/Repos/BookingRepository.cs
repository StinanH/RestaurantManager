using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RestaurantManager.Data.Repos.IRepos;
using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.BookingDTOs;
using System.Diagnostics.Eventing.Reader;
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

        public async Task<IEnumerable<BookingCreateDTO>> GetAvaliableBookingsOnDayAsync(int restaurantId, int numberOfPeople, DateTime date)
        {
            int openingHour = 10;
            int closingHour = 22;

            List<BookingCreateDTO> possibleBookingDTOs = new List<BookingCreateDTO>();

            // Getting all bookings at the restaurant
            var bookingsAtRestaurant = await _context.Bookings
                .Where(b => b.Restaurant.Id == restaurantId)
                .Include(b => b.Table)
                .Include(b => b.Timeslot)
                .ToListAsync();

            var tablesAtRestaurant = await _context.Tables
                .Where(t => t.FK_RestaurantId == restaurantId)
                .OrderBy(t => t.NrOfSeats)
                .ToListAsync();

            for (int hour = openingHour; hour <= closingHour - 2; hour++)
            {
                DateTime slotStarttime = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
                DateTime slotEndtime = new DateTime(date.Year, date.Month, date.Day, hour + 2, 0, 0);

                var bookingsAtTime = bookingsAtRestaurant
                    .Where(b => b.Timeslot.StartTime <= slotStarttime && b.Timeslot.EndTime >= slotEndtime)
                    .ToList();

                // Filter the tables before the loop
                var availableTables = tablesAtRestaurant
                    .Where(t => t.NrOfSeats >= numberOfPeople)  // Only considers tables with enough seats
                    .ToList();

                // Remove tables that are already booked at this time
                foreach (var booking in bookingsAtTime)
                {
                    availableTables.RemoveAll(t => t.Id == booking.FK_TableId); // Removes all bookings for the same table
                }

                if (availableTables.Count == 0)
                {
                    continue; // Skip this slot if no tables are available
                }

                // Add a new booking option for the available time slot
                possibleBookingDTOs.Add(new BookingCreateDTO
                {
                    RestaurantId = restaurantId,
                    requestedTime = slotStarttime,
                    UserId = 6,  // Adjust as needed
                    NrOfPeople = numberOfPeople,
                    Requests = "" // Handle requests if needed
                });
            }

            return possibleBookingDTOs;
        }




        //should return a list of avaliable tables
        public async Task<Table> IsBookingAvaliable(Booking booking)
        {
            var BookingsAtThatTimeList = await _context.Bookings
                .Where(b => b.Restaurant.Id == booking.FK_RestaurantId && b.Timeslot.StartTime <= booking.requestedTime && b.Timeslot.EndTime >= booking.requestedEndTime)
                .Include(b => b.Table)
                .ToListAsync();

            var TablesAtRestaurant = await _context.Tables
                .Where(t => t.FK_RestaurantId == booking.FK_RestaurantId)
                .OrderBy(t => t.NrOfSeats)
                .ToListAsync();

            if (BookingsAtThatTimeList.Count >= TablesAtRestaurant.Count)
            {
                //all tables are booked
                return new Table();
            }

            else
            {
                //get ids of tables booked at that time.
                var bookedTableIds = BookingsAtThatTimeList.Select(b => b.FK_TableId).Distinct().ToList() ?? new List<int>();

                //save avaliable tables to a list
                var avaliableTables = TablesAtRestaurant.Where(t => !bookedTableIds.Contains(t.Id)).ToList();

                for (int i = avaliableTables.Count - 1; i >= 0; i--)
                {

                    if (avaliableTables[i].NrOfSeats < booking.NrOfPeople)
                    {
                        avaliableTables.RemoveAt(i);

                    }

                    if (avaliableTables.Count == 1)
                    {

                        return avaliableTables[0];
                    }
                }

                return avaliableTables[0];
            }
        }

        //make task a bool
        public async Task AddBookingAsync(Booking booking)
        {
            Console.WriteLine("omg it's adding booking.");

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

            var timeslot = await _context.TimeSlots.FirstOrDefaultAsync(t => booking.FK_TimeslotId == t.Id);

            if(timeslot == null)
            {
                //return false?? no timeslot found with that id.
            }

            booking.Restaurant = restaurant;
            booking.User = user;
            booking.Timeslot = timeslot;
            
            restaurant.Bookings.Add(booking);

            user.Bookings.Add(booking);
            restaurant.Bookings.Add(booking);

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
