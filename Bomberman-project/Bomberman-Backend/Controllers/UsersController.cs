using Bomberman_Backend.Services.Interfaces;
using DomainModels;
using DomainModels.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Bomberman_Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _users;
        public UsersController(IUserService users)
        {
            _users = users;
        }
        [HttpGet]
        public ActionResult<GetUserDTO> Get([FromQuery] Guid id)
        {
            var _user = _users.GetUser(id);
            if (Object.ReferenceEquals(_user, null))
            {
                return NotFound("User not found");
            }
            return _user;
        }

        [HttpGet("/api/[controller]/all")]
        public ActionResult<List<GetUserDTO>> GetAll()
        {
            var users = _users.GetUsers();
            if (Object.ReferenceEquals(users, null))
            {
                return NotFound("No users found");
            }
            return users;
        }

        [HttpPut("info")]
        public ActionResult<UpdateUserInfoDTO> PutInfo([FromQuery] Guid id, [FromBody] UpdateUserInfoDTO user)
        {
            var _user = _users.UpdateUser(id, user);
            if (Object.ReferenceEquals(_user, null))
            {
                return NotFound("User not found");
            }
            return _user;
        }

        [HttpPut("password")]
        public ActionResult<UpdateUserPasswordDTO> PutPassword([FromQuery] Guid id, [FromBody] string password)
        {
            var _user = _users.UpdateUserPassword(id, password);
            if (Object.ReferenceEquals(_user, null))
            {
                return NotFound("User not found");
            }
            return _user;
        }

        [HttpDelete]
        public ActionResult Delete([FromQuery] Guid id)
        {
            _users.DeleteUser(id);
            return Ok();
        }
    }
}
