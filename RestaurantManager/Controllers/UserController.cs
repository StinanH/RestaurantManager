using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Models.DTOs.UserDTOs;
using RestaurantManager.Services.IServices;

namespace RestaurantManager.Controllers
{
    [ApiController]
    [Route("[controller]")]

    //using controller instead of controllerbase as api is going to be used to render view rendering and json responses.
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        [Route("/create")]
        public async Task<ActionResult> CreateUser (UserCreateDTO userDTO)
        {
            await _userServices.AddUserAsync(userDTO);

            return Ok("User for : "+userDTO.Name+" created.");
        }

        [HttpGet]
        [Route("/all_users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userServices.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _userServices.GetUserAsync(userId);

            return Ok(user);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser(int userId, UserUpdateDTO userDTO)
        {
            await _userServices.UpdateUserAsync(userDTO);
            
            return Ok("User updated.");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _userServices.DeleteUserAsync(userId);

            return Ok("User deleted.");
        }


    }
}
