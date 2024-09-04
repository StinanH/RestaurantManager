using Microsoft.AspNetCore.Identity;
using RestaurantManager.Data.Repos;
using RestaurantManager.Data.Repos.IRepos;
using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.RestaurantDTOs;
using RestaurantManager.Models.DTOs.TableDTOs;
using RestaurantManager.Services.IServices;

namespace RestaurantManager.Services
{
    public class RestaurantServices : IRestaurantServices
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ITableRepository _tableRepository;

        public RestaurantServices(IRestaurantRepository restaurantRepository, ITableRepository tableRepository)
        {
            _restaurantRepository = restaurantRepository;  
            _tableRepository = tableRepository;
        }

        public async Task<IEnumerable<RestaurantGetDTO>> GetAllRestaurantsAsync()
        { 
            var allRestaurants = await _restaurantRepository.GetAllRestaurantsAsync();

            var restaurantList = allRestaurants.Select(r => new RestaurantGetDTO
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Address = r.Address,
                Email = r.Email,
                PhoneNumber = r.PhoneNumber,
            }).ToList();

            return restaurantList;
        }
        public async Task<RestaurantGetDTO> GetRestaurantAsync(int restaurantID)
        {
            var restaurantById = await _restaurantRepository.GetRestaurantAsync(restaurantID);

            var restaurant = new RestaurantGetDTO
            {
                Id = restaurantById.Id,
                Name = restaurantById.Name,
                Description = restaurantById.Description,
                Address = restaurantById.Address,
                Email = restaurantById.Email,
                PhoneNumber = restaurantById.PhoneNumber,
            };

            return restaurant;
        }

        //return bool on these 3 to report success?

        public async Task AddRestaurantAsync(RestaurantCreateDTO restaurantDTO)
        {
            //check if any of the required fields are empty (DTO.name), if so return false.

            var restaurantToAdd = new Restaurant
            {
                Name = restaurantDTO.Name,
                Description = restaurantDTO.Description,
                Address = restaurantDTO.Address,
                Email = restaurantDTO.Email,
                PhoneNumber = restaurantDTO.PhoneNumber
            };

            await _restaurantRepository.AddRestaurantAsync(restaurantToAdd);

            //return true;
        }
        public async Task UpdateRestaurantAsync(RestaurantUpdateDTO restaurantDTO)
        {
            //check if any of the required fields are empty (DTO.name), if so return false.
            
            var restaurantToUpdate = await _restaurantRepository.GetRestaurantAsync(restaurantDTO.Id);

            //check if ToUpdate result found == null, if so return false

            restaurantToUpdate.Name = restaurantDTO.Name;
            restaurantToUpdate.Address = restaurantDTO.Address;
            restaurantToUpdate.Email = restaurantDTO.Email;
            restaurantToUpdate.Description = restaurantDTO.Description;
            restaurantToUpdate.PhoneNumber = restaurantDTO.PhoneNumber;

            await _restaurantRepository.UpdateRestaurantAsync(restaurantToUpdate);

        }
        public async Task DeleteRestaurantAsync(int restaurantId)
        {
            var restaurantToDelete = await _restaurantRepository.GetRestaurantAsync(restaurantId);

            //check if toDelete == nulll, if so return false

            await _restaurantRepository.DeleteRestaurantAsync(restaurantToDelete);

            //return true;
        }
    }
}
