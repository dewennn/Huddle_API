using Huddle.Interfaces;
using Huddle.Models;
using Huddle.Services;
using Microsoft.AspNetCore.Mvc;

namespace Huddle.Controllers
{
    [Route("api")]
    public class PersonalMessageController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPersonalMessageService _pmService;

        public PersonalMessageController(
            IUserService userService,
            IPersonalMessageService pmService)
        {
            _userService = userService;
            _pmService = pmService;
        }

        private Guid? AuthenticateUser()
        {
            var token = Request.Cookies["jwt"];
            return _userService.ValidateToken(token);
        }

        [HttpGet("{id}/messages")]
        public async Task<ActionResult<List<PersonalMessages>>> GetMessages(Guid id)
        {
            var userId = AuthenticateUser();
            if (userId == null) return Unauthorized();

            var messages = await _pmService.GetMessage(userId.Value, id);

            if (messages == null) return BadRequest();
            return Ok(messages);
        }

        [HttpPost("{id}/add_message")]
        public async Task<ActionResult<List<PersonalMessages>>> PostMessage(Guid id, [FromBody] Dictionary<string, string> request)
        {
            var userId = AuthenticateUser();
            if (userId == null) return Unauthorized();
            string content = request["content"];

            try
            {
                await _pmService.PostMessage(userId.Value, id, content);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
