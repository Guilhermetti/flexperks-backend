using FlexPerks.Application.Commands.Users;
using FlexPerks.Application.Handlers.Users;
using FlexPerks.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexPerks.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _users;
        private readonly CreateUserHandler _create;

        public UsersController(
            IUserRepository users,
            CreateUserHandler create)
        {
            _users = users;
            _create = create;
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _users.GetById(id);
            if (user is null)
                return NotFound();

            return Ok(new { user.Id, user.Name, user.Email, user.CompanyId });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand cmd)
        {
            var id = await _create.Handle(cmd);
            if (!_create.IsValid)
                return BadRequest(_create.Notifications);

            return CreatedAtAction(nameof(Get), new { id = id!.Value }, new { id });
        }
    }
}
