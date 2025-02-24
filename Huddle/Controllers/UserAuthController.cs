using Microsoft.AspNetCore.Mvc;
using Huddle.Interfaces;

namespace Huddle.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserAuthController(IUserService userService)
        {
            _userService = userService;
        }

        // LOGIN VIA EMAIL & PASSWORD
        [HttpPost("login")]
        public async Task<ActionResult> ValidateUserByEmailPass(
            [FromBody] Dictionary<string, string> request
        ){
            var token = await _userService.ValidateUserByEmailPass(
                request["email"], request["password"]);

            if (token == null) {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            return Ok( new { token });
        }

        // REGISTER NEW USER
        [HttpPost("register")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            await _userService.AddUser(user);
            return Ok();
        }

    }
}
