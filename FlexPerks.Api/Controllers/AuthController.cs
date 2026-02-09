using FlexPerks.Application.Commands.Auth;
using FlexPerks.Application.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexPerks.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginHandler _login;
        public AuthController(LoginHandler login) => _login = login;

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCommand cmd)
        {
            var result = await _login.Handle(cmd);
            if (!_login.IsValid)
                return BadRequest(_login.Notifications);

            return Ok(result);
        }
    }
}
