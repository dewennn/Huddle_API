using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Huddle.Context;
using Microsoft.AspNetCore.Authorization;
using Huddle.Interfaces;
using System.Security.Claims;

namespace Huddle.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // api/user/me | GET USER DATA
        [HttpGet("me")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (id == null) return Unauthorized("Invalid token.");

            var user = await _userService.GetUserByID(new Guid(id));
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        //// UPDATE USER DATA
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(Guid id, User user)
        //{
            
        //}

        //// DELETE USER
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(Guid id)
        //{
            
        //}
    }
}
