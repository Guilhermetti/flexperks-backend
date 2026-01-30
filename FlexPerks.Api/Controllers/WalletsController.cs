using FlexPerks.Application.Commands.Wallets;
using FlexPerks.Application.Handlers.Wallets;
using FlexPerks.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexPerks.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WalletsController : ControllerBase
    {
        private readonly IPerksWalletRepository _repo;
        private readonly CreateWalletHandler _create;

        public WalletsController(
            IPerksWalletRepository repo,
            CreateWalletHandler create)
        {
            _repo = repo;
            _create = create;
        }

        [HttpGet]
        public async Task<IActionResult> ListByUser([FromQuery] int userId)
        {
            var list = await _repo.ListByUserId(userId);
            return Ok(list.Select(w => new { w.Id, w.UserId, w.CategoryId, w.Balance }));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateWalletCommand cmd)
        {
            var id = await _create.Handle(cmd);
            if (!_create.IsValid)
                return BadRequest(_create.Notifications);

            return Created($"/api/wallets/{id}", new { id });
        }
    }
}
