using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Huddle.Interfaces;
using System.Security.Claims;
using Huddle.Models;
using Huddle.Services;

namespace Huddle.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // api/user/me | GET USER DATA w JWT
        [HttpGet("me")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            var token = Request.Cookies["jwt"];

            var userId = _userService.ValidateToken(token);

            if ( userId == null ) return Unauthorized();

            var user = await _userService.GetUserByID(userId);

            return Ok(user);
        }

        // api/user/friends | GET USER FRIEND LIST w JWT
        [HttpGet("friends")]
        public async Task<ActionResult<List<User>?>> GetFriends()
        {
            var token = Request.Cookies["jwt"];
            var userId = _userService.ValidateToken(token);

            if (userId == null) return Unauthorized("Invalid token.");

            var friendships = await _userService.GetUserFriendList(userId);

            if (friendships == null) return NotFound();

            return Ok(friendships);
        }
    }
}
