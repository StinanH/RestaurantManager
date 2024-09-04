using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RestaurantManager.Data.Repos.IRepos;
using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.BookingDTOs;

namespace RestaurantManager.Data.Repos
{
    public class TableRepository : ITableRepository
    {
        private readonly RestaurantManagerContext _context;

        public TableRepository(RestaurantManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Table>> GetAllTables()
        {
            var tableList = await _context.Tables
                .ToListAsync();

            return tableList;
        }

        public async Task<IEnumerable<Table>> GetAllTablesAtRestaurant(int restaurantId)
        {
            var tableList = await _context.Tables
                .Where(t => t.FK_RestaurantId == restaurantId)
                .ToListAsync();

            return tableList;
        }

        public async Task<int> GetAvaliableTableId(BookingCreateDTO bookingDTO)
        {
            DateTime endtime = bookingDTO.requestedTime.AddHours(2);

            var AllTableIds = await _context.Tables
                .Where(t => t.FK_RestaurantId == bookingDTO.RestaurantId)
                .Select(t => t.Id)
                .Distinct()
                .ToListAsync();

            var BookedTableIds = await _context.Bookings
                .Where(b => b.Restaurant.Id == bookingDTO.RestaurantId && b.Timeslot.StartTime < endtime && b.Timeslot.EndTime > bookingDTO.requestedTime)
                .Select(b => b.FK_TableId)
                .Distinct()
                .ToListAsync();

            int AvaliableId = AllTableIds.FirstOrDefault(number => !BookedTableIds.Contains(number));

            return AvaliableId;
        }
        public async Task<Table> GetTableAsync(int tableId)
        {
            var table = await _context.Tables
                .FirstOrDefaultAsync(t => t.Id == tableId);

            return table;
        }
        public async Task AddTableAsync(Table table)
        {
            await _context.Tables.AddAsync(table);

            await _context.SaveChangesAsync();

            Restaurant restaurantTableIsAt = await _context.Restaurants.FirstOrDefaultAsync(r => table.FK_RestaurantId == r.Id);

            restaurantTableIsAt.Tables.Add(table);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateTableAsync(Table table)
        {
            _context.Tables.Update(table);

            await _context.SaveChangesAsync();
        }
        public async Task DeleteTableAsync(Table table)
        {
            _context.Tables.Remove(table);

            await _context.SaveChangesAsync();
        }

    }
}
