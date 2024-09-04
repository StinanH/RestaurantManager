using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Models.DTOs.TableDTOs;
using RestaurantManager.Services.IServices;

namespace RestaurantManager.Controllers
{
    [ApiController]
    [Route("restaurant/{restaurantId:int}/[controller]")]
    public class TableController : Controller
    {
        private readonly ITableServices _tableServices;

        public TableController(ITableServices tableServices)
        {
            _tableServices = tableServices;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateTable(TableCreateDTO tableDTO)
        {
            await _tableServices.AddTableAsync(tableDTO);

            return Ok("Table for "+tableDTO.NrOfSeats+" created.");
        }

        [HttpGet]
        [Route("all_tables")]
        public async Task<IActionResult> GetAllTables()
        {
            var tables = await _tableServices.GetAllTables();

            return Ok(tables);
        }

        [HttpGet]
        [Route("{tableId:int}")]
        public async Task<IActionResult> GetTableAsync(int tableId)
        {
            var table = await _tableServices.GetTableAsync(tableId);

            return Ok(table);
        }

        [HttpPut]
        [Route("{tableId:int}")]
        public async Task<IActionResult> UpdateRestaurant(int tableId, TableUpdateDTO tableDTO)
        {
            await _tableServices.UpdateTableAsync(tableDTO);

            return Ok("Tableinfo updated.");
        }

        [HttpDelete]
        [Route("{tableId:int}")]
        public async Task<IActionResult> DeleteRestaurant(int tableId)
        {
            await _tableServices.DeleteTableAsync(tableId);

            return Ok("Table deleted.");
        }
    }
}
