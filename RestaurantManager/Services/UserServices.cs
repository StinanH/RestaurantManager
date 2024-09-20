using RestaurantManager.Data.Repos.IRepos;
using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.UserDTOs;
using RestaurantManager.Services.IServices;

namespace RestaurantManager.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository) 
        { 
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserGetDTO>> GetAllUsersAsync()
        {
            var allUsers = await _userRepository.GetAllUsersAsync();

            var userlist = allUsers.Select(u => new UserGetDTO
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            }).ToList();

            return userlist;
        }
        public async Task<UserGetDTO> GetUserAsync(int userId)
        {
            var userById = await _userRepository.GetUserAsync(userId);

            var user = new UserGetDTO
            {
                Id = userById.Id,
                Name = userById.Name,
                Email = userById.Email,
                PhoneNumber = userById.PhoneNumber
            };

            return user;
        }

        //return bool on these 4 to report success?
        
        public async Task AddUserAsync(UserCreateDTO userDTO)
        {
            var userToAdd = new User
            {
                Name = userDTO.Name,
                Email = userDTO.Email
            };

            await _userRepository.AddUserAsync(userToAdd);
        }

        public async Task UpdateUserAsync(UserUpdateDTO userDTO)
        {
            //check if any of the required fields are empty (DTO.name), if so return false.

            var userToUpdate = await _userRepository.GetUserAsync(userDTO.Id);

            //check if ToUpdate result found == null, if so return false

            userToUpdate.Name = userDTO.Name;
            userToUpdate.Email = userDTO.Email;
            userToUpdate.PhoneNumber = userDTO.PhoneNumber;

            await _userRepository.UpdateUserAsync(userToUpdate);
        }
        public async Task DeleteUserAsync(int userId)
        {
            var userById = await _userRepository.GetUserAsync(userId);

            await _userRepository.DeleteUserAsync(userById);
        }
    }
}
