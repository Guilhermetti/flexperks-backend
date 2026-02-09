using FlexPerks.Application.Commands.Categories;
using FlexPerks.Application.Handlers;
using FlexPerks.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexPerks.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly IBenefitCategoryRepository _repo;
        private readonly CategoryHandler _create;

        public CategoriesController(
            IBenefitCategoryRepository repo,
            CategoryHandler create)
        {
            _repo = repo;
            _create = create;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cats = await _repo.ListAll();
            return Ok(cats.Select(c => new { c.Id, c.Name }));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryCommand cmd)
        {
            var id = await _create.Handle(cmd);
            if (!_create.IsValid)
                return BadRequest(_create.Notifications);

            return Created($"/api/categories/{id}", new { id });
        }
    }
}
