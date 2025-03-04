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
using Huddle.DTOs;

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

        private Guid? AuthenticateUser()
        {
            var token = Request.Cookies["jwt"];
            return _userService.ValidateToken(token);
        }

        // api/user/me | GET USER DTO
        [HttpGet("me")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var userId = AuthenticateUser();
            if ( userId == null ) return Unauthorized();

            var user = await _userService.GetUserDTO(userId);

            return Ok(user);
        }

        // api/user/me | GET USER DTO
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserDTO(Guid id)
        {
            var userId = AuthenticateUser();
            if (userId == null) return Unauthorized();

            var user = await _userService.GetUserDTO(id);

            return Ok(user);
        }

        // api/user/friends | GET USER SENT FRIEND REQUEST
        [HttpGet("sent_friend_requests")]
        public async Task<ActionResult<List<UserDTO>?>> GetSentFriendRequests()
        {
            var userId = AuthenticateUser();
            if (userId == null) return Unauthorized("Invalid token.");

            var sentFriendRequest = await _userService.GetUserSentFriendRequest(userId);

            if (sentFriendRequest == null) return NotFound();

            return Ok(sentFriendRequest);
        }

        // api/user/friends | GET USER RECEIVED FRIEND REQUEST
        [HttpGet("received_friend_requests")]
        public async Task<ActionResult<List<UserDTO>?>> GetReceivedFriendRequests()
        {
            var userId = AuthenticateUser();
            if (userId == null) return Unauthorized("Invalid token.");

            var receivedFriendRequest = await _userService.GetUserReceivedFriendRequest(userId);

            if (receivedFriendRequest == null) return NotFound();

            return Ok(receivedFriendRequest);
        }

        // api/user/friends | GET USER FRIEND LIST
        [HttpGet("friends")]
        public async Task<ActionResult<List<UserDTO>?>> GetFriends()
        {
            var userId = AuthenticateUser();
            if (userId == null) return Unauthorized("Invalid token.");

            var friendships = await _userService.GetUserFriendList(userId);

            if (friendships == null) return NotFound();

            return Ok(friendships);
        }

        // api/user/add_friend | POST, ADD NEW FRIENDSHIP REQUEST
        [HttpPost("send_friend_request")]
        public async Task<ActionResult> SendFriendRequest([FromBody] Dictionary<string, string> request)
        {
            var userId = AuthenticateUser();
            if (userId == null) return Unauthorized("Invalid token.");

            string targetUsername = request["targetUsername"];

            try
            {
                await _userService.AddFriendRequestWithUsername(userId.Value, targetUsername);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
        // api/user/accept_friend_request | POST, ADD NEW FRIENDSHIP
        [HttpPost("accept_friend_request")]
        public async Task<ActionResult> AcceptFriendRequest([FromBody] Dictionary<string, string> request)
        {
            var userId = AuthenticateUser();
            if (userId == null) return Unauthorized("Invalid token.");

            string friendId = request["friendId"];
            string receiver = request["receiver"];

            var senderId = userId.Value;
            var receiverId = new Guid(friendId);
            if (receiver == "1")
            {
                receiverId = userId.Value;
                senderId = new Guid(friendId);
            }

            try
            {
                await _userService.RemoveFriendshipRequest(senderId, receiverId);
                await _userService.AddFriendship(senderId, receiverId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // api/user/reject_friend_request | DELETE, REMOVE FRIENDSHIP REQUEST
        [HttpDelete("reject_friend_request")]
        public async Task<ActionResult> RejectFriendRequest([FromBody] Dictionary<string, string> request)
        {
            var userId = AuthenticateUser();
            if (userId == null) return Unauthorized("Invalid token.");

            string friendId = request["friendId"];
            string receiver = request["receiver"];

            var senderId = userId.Value;
            var receiverId = new Guid(friendId);
            if (receiver == "1")
            {
                receiverId = userId.Value;
                senderId = new Guid(friendId);
            }

            try
            {
                await _userService.RemoveFriendshipRequest(senderId, receiverId);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // api/user/remove_friend | DELETE, REMOVE FRIEND
        [HttpDelete("remove_friend")]
        public async Task<ActionResult> RemoveFriend([FromBody] Dictionary<string, string> request)
        {
            var userId = AuthenticateUser();
            if (userId == null) return Unauthorized("Invalid token.");

            string friendId = request["friendId"];

            try
            {
                await _userService.RemoveFriendship(userId.Value, new Guid(friendId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
