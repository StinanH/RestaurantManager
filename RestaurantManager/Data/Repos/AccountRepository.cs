using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantManager.Data.Repos.IRepos;
using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.AccountDTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;

namespace RestaurantManager.Data.Repos
{
    public class AccountRepository : IAccountRepository
    {

        private readonly RestaurantManagerContext _context;
        private readonly IConfiguration _configuration;

        public AccountRepository(RestaurantManagerContext context, IConfiguration configuration)
        {

            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> CheckIfUserExists(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        public async Task<Account> GetAccountByEmail(string Email)
        {
            var acc = await _context.Accounts
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Email == Email)
                ;

            return acc;
        }

        public async Task AddAccountAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GenerateJwtToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            ClaimsIdentity claims;

            if (account.isAdmin) 
            {
                claims = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, $"{account.User.Name}"),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Email, account.Email)
                });
            }

            else
            {
                claims = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, $"{account.User.Name}"),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.Email, account.Email)
                });
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
