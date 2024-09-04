using RestaurantManager.Models;
using RestaurantManager.Data.Repos;
using RestaurantManager.Models.DTOs.BookingDTOs;

namespace RestaurantManager.Data.Repos.IRepos
{
    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetAllTables();
        Task<IEnumerable<Table>> GetAllTablesAtRestaurant(int restaurantId);
        Task<Table> GetTableAsync(int tableId);
        Task<int> GetAvaliableTableId(BookingCreateDTO bookingDTO);
        Task AddTableAsync(Table table);
        Task UpdateTableAsync(Table table);
        Task DeleteTableAsync(Table table);
    }
}
