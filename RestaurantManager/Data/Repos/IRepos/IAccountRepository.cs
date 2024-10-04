using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.AccountDTOs;

namespace RestaurantManager.Data.Repos.IRepos
{
    public interface IAccountRepository
    {
        Task AddAccountAsync(Account account);
        Task<bool> CheckIfUserExists(string email);

        Task<Account> GetAccountByEmail(string email);

        Task<string> GenerateJwtToken(Account account);
    }
}
