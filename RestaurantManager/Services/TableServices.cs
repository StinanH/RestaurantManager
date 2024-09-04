using RestaurantManager.Data.Repos;
using RestaurantManager.Data.Repos.IRepos;
using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.TableDTOs;
using RestaurantManager.Services.IServices;

namespace RestaurantManager.Services
{
    public class TableServices : ITableServices
    {
        private readonly ITableRepository _tableRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        public TableServices(ITableRepository tableRepository, IRestaurantRepository restaurantRepository)
        {
            _tableRepository = tableRepository;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<IEnumerable<TableGetDTO>> GetAllTables()
        {
            var allTables = await _tableRepository.GetAllTables();

            var tableList = allTables.Select(t => new TableGetDTO
            {
                Id = t.Id,
                NrOfSeats = t.NrOfSeats,
            });

            return tableList;
        }
        public async Task<IEnumerable<TableGetDTO>> GetAllTablesAtRestaurant(int restaurantId)
        {
            IEnumerable<Table> allTables = await _tableRepository.GetAllTablesAtRestaurant(restaurantId);

            var tableList = allTables.Select(t => new TableGetDTO
            {
                Id = t.Id,
                NrOfSeats = t.NrOfSeats,
            });

            return tableList;
        }
        public async Task<TableGetDTO> GetTableAsync(int tableId)
        {
            var tableById = await _tableRepository.GetTableAsync(tableId);

            var table = new TableGetDTO
            {
                Id = tableById.Id,
                NrOfSeats = tableById.NrOfSeats
            };

            return table;
        }
        public async Task AddTableAsync(TableCreateDTO tableDTO)
        {
            var restaurant = await _restaurantRepository.GetRestaurantAsync(tableDTO.RestaurantId);

            //check if restaurant is null

            var tableToAdd = new Table
            {
                FK_RestaurantId = tableDTO.RestaurantId,
                NrOfSeats = tableDTO.NrOfSeats
            };

            await _tableRepository.AddTableAsync(tableToAdd);

        }
        public async Task UpdateTableAsync(TableUpdateDTO tableDTO)
        {
            var tableToUpdate = await _tableRepository.GetTableAsync(tableDTO.TableId);

            tableToUpdate.NrOfSeats = tableDTO.NrOfSeats;

            await _tableRepository.UpdateTableAsync(tableToUpdate);
        }
        public async Task DeleteTableAsync(int tableId)
        {
            var tableToDelete = await _tableRepository.GetTableAsync(tableId);

            await _tableRepository.DeleteTableAsync(tableToDelete);
        }
    }
}
