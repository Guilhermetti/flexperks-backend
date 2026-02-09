using FlexPerks.Application.Commands.Employees;
using FlexPerks.Application.Handlers;
using FlexPerks.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexPerks.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employees;
        private readonly EmployeeHandler _create;

        public EmployeesController(IEmployeeRepository employees, EmployeeHandler create)
        {
            _employees = employees;
            _create = create;
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var emp = await _employees.GetById(id);
            if (emp is null)
                return NotFound();

            return Ok(new
            {
                emp.Id,
                emp.CompanyId,
                emp.FullName,
                emp.Email,
                emp.Document,
                emp.Registration,
                emp.HireDate,
                emp.TerminationDate,
                emp.ManagerId
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CreateEmployeeCommand cmd)
        {
            var id = await _create.Handle(cmd);
            if (!_create.IsValid)
                return BadRequest(_create.Notifications);

            return CreatedAtAction(nameof(Get), new { id = id!.Value }, new { id });
        }
    }
}
