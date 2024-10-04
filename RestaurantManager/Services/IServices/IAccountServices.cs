using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Models.DTOs.AccountDTOs;

namespace RestaurantManager.Services.IServices
{
    public interface IAccountServices
    {
        Task<bool> RegisterAccountAsync(AccountCreateDTO accountCreateDTO);

        Task<(bool, string)> Login(AccountLoginDTO accountLoginDTO);
    }
}
