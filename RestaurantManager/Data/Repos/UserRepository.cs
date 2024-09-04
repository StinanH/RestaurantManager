using Microsoft.EntityFrameworkCore;
using RestaurantManager.Data.Repos.IRepos;
using RestaurantManager.Models;

namespace RestaurantManager.Data.Repos
{
    public class UserRepository : IUserRepository
    {
        public readonly RestaurantManagerContext _context;

        public UserRepository(RestaurantManagerContext context)
        {

            _context = context;
        }

        //Get all users
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var userList = await _context.Users.ToListAsync();

            return userList;
        }

        //Get a user by ID
        public async Task<User> GetUserAsync(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        //Add new User
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();
        }

        //Update User
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);

            await _context.SaveChangesAsync();

        }

        //Delete User
        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }
    }
}
