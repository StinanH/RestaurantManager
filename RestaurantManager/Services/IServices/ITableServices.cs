using RestaurantManager.Models.DTOs.TableDTOs;

namespace RestaurantManager.Services.IServices
{
    public interface ITableServices
    {
        Task<IEnumerable<TableGetDTO>> GetAllTables();
        Task<IEnumerable<TableGetDTO>> GetAllTablesAtRestaurant(int restaurantId);
        Task<TableGetDTO> GetTableAsync(int tableId);
        Task AddTableAsync(TableCreateDTO tableDTO);
        Task UpdateTableAsync(TableUpdateDTO tableDTO);
        Task DeleteTableAsync(int tableId);
    }
}
