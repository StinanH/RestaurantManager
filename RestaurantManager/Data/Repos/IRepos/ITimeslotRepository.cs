using RestaurantManager.Models;

namespace RestaurantManager.Data.Repos.IRepos
{
    public interface ITimeslotRepository
    {
        Task<TimeSlot> AddTimeSlotAsync(TimeSlot timeSlot);
        Task UpdateTimeSlotAsync(TimeSlot timeSlot);
        Task DeleteTimeSlotAsync(TimeSlot timeSlot);
    }
}
