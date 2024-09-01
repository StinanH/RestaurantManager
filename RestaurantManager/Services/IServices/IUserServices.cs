using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.UserDTOs;

namespace RestaurantManager.Services.IServices
{
    public interface IUserServices
    {
        Task<IEnumerable<UserGetDTO>> GetAllUsersAsync();
        Task<UserGetDTO> GetUserAsync(int userId);
        Task AddUserAsync(UserCreateDTO userDTO);
        Task UpdateUserAsync(UserUpdateDTO userDTO);
        Task DeleteUserAsync(int userId);
    }
}
