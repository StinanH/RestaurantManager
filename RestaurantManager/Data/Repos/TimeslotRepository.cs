using RestaurantManager.Data.Repos.IRepos;
using RestaurantManager.Models;

namespace RestaurantManager.Data.Repos
{
    public class TimeslotRepository : ITimeslotRepository
    {
        private readonly RestaurantManagerContext _context; 

        public TimeslotRepository(RestaurantManagerContext context)
        {
            _context = context;
        }

        public async Task<TimeSlot> AddTimeSlotAsync(TimeSlot timeSlot)
        {
            await _context.TimeSlots.AddAsync(timeSlot);

            await _context.SaveChangesAsync();
            
            return timeSlot;
        }
        public async Task UpdateTimeSlotAsync(TimeSlot timeSlot)
        {
            _context.TimeSlots.Update(timeSlot);

            await _context.SaveChangesAsync();
        }
        public async Task DeleteTimeSlotAsync(TimeSlot timeSlot)
        {
            _context.TimeSlots.Remove(timeSlot);

            await _context.SaveChangesAsync();
        }
    }
}
