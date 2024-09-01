using Microsoft.AspNetCore.Mvc;

namespace RestaurantManager.Controllers
{
    [ApiController]
    [Route("user/[controller]")]

    //using controller instead of controllerbase as api is going to be used to render view rendering and json responses.
    public class UserController : Controller
    {


        /*
        [HttpGet]
        [Route("users")]
        public IActionResult GetAllUsers()
        {
            var users = await GetAllUsersAsync();
            return View(users);
        }
        */

    }
}
