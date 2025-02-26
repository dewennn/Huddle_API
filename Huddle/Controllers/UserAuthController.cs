using Microsoft.AspNetCore.Mvc;
using Huddle.Interfaces;
using Huddle.Models;
using System.Diagnostics;

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
        public async Task<IActionResult> ValidateUserByEmailPass(
            [FromBody] Dictionary<string, string> request
        ){
            var token = await _userService.ValidateUserByEmailPass(
                request["email"], request["password"]);

            if (token == null) {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(1)
            };

            Response.Cookies.Append("jwt", token, cookieOptions);

            return Ok(new { message = "Login Successful!" });
        }

        // REGISTER NEW USER
        [HttpPost("register")]
        public async Task<ActionResult<string?>> PostUser(User user)
        {
            var token = await _userService.AddUser(user);

            if (token == null)
            {
                return Unauthorized(new { message = "Registration failed" });
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(1)
            };

            Response.Cookies.Append("jwt", token, cookieOptions);

            return Ok(new { message = "Register Successful!" });
        }

    }
}
