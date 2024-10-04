using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Data.Repos;
using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.AccountDTOs;
using RestaurantManager.Models.DTOs.UserDTOs;
using RestaurantManager.Services.IServices;
using RestaurantManager.Data.Repos.IRepos;

namespace RestaurantManager.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;

        public AccountServices(IUserRepository userRepository, IAccountRepository accountRepository) 
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
        }

        public async Task<bool> RegisterAccountAsync(AccountCreateDTO accountCreate)
        {
            //check if account with user email allready exists
            var allreadyExists = await _accountRepository.CheckIfUserExists(accountCreate.Email);

            if (allreadyExists == false)
            {
                var newUser = new User
                {
                    Name = accountCreate.Name,
                    Email = accountCreate.Email,
                };

                //add new user to connect to acc
                await _userRepository.AddUserAsync(newUser);

                //hash password
                string passwordhash = BCrypt.Net.BCrypt.HashPassword(accountCreate.Password);

                var newAccount = new Account
                {
                    Email = accountCreate.Email,
                    isAdmin = false,
                    PasswordHashed = passwordhash,
                    FK_User = newUser.Id
                };

                //add new account
                await _accountRepository.AddAccountAsync(newAccount);

                return true;
            }

            else
            {
                return false;
            }
        }

        public async Task<(bool, string)> Login(AccountLoginDTO accountLoginDTO)
        {
            var acc = await _accountRepository.GetAccountByEmail(accountLoginDTO.Email);

            //if no account or password doesn't match
            if (acc == null)
            {
                return (false, "account doesn't exist");
            }

            else if (!BCrypt.Net.BCrypt.Verify(accountLoginDTO.Password, acc.PasswordHashed))
            {
                return (false, "password is incorrect");
            }

            var token = await _accountRepository.GenerateJwtToken(acc);

            return (true, token);

        }

    }
}
