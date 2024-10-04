using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Models.DTOs.AccountDTOs;
using RestaurantManager.Services;
using RestaurantManager.Services.IServices;

namespace RestaurantManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountServices _accountServices;

        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        [HttpPost("RegisterAccount")]
        public async Task<IActionResult> RegisterAccount(AccountCreateDTO accountCreateDTO)
        {
            var successfull = await _accountServices.RegisterAccountAsync(accountCreateDTO);

            if (successfull)
            {
                return Ok();
            }

            else
            {
                return BadRequest();
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AccountLoginDTO accountLoginDTO)
        {
            (bool successfull, string reason) = await _accountServices.Login(accountLoginDTO);

            if (!successfull)
            {

                return Unauthorized(reason);
            }

            else
            {
                string token = reason;
                return Ok(new { token});
            }
        }
    }
}
