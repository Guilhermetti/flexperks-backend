using FlexPerks.Application.Commands.TimeClock;
using FlexPerks.Application.Handlers;
using FlexPerks.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexPerks.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TimeClockController : ControllerBase
    {
        private readonly ITimeClockEntryRepository _entries;
        private readonly TimeClockEntryHandler _create;

        public TimeClockController(ITimeClockEntryRepository entries, TimeClockEntryHandler create)
        {
            _entries = entries;
            _create = create;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTimeClockEntryCommand cmd)
        {
            var id = await _create.Handle(cmd);
            if (!_create.IsValid)
                return BadRequest(_create.Notifications);

            return CreatedAtAction(nameof(GetByEmployeePeriod), new { companyId = cmd.CompanyId, employeeId = cmd.EmployeeId }, new { id });
        }

        // Exemplo: /api/timeclock/employee/10?companyId=1&fromUtc=2026-02-01T00:00:00Z&toUtc=2026-03-01T00:00:00Z
        [HttpGet("employee/{employeeId:int}")]
        public async Task<IActionResult> GetByEmployeePeriod(
            int employeeId,
            [FromQuery] int companyId,
            [FromQuery] DateTime fromUtc,
            [FromQuery] DateTime toUtc)
        {
            if (companyId <= 0) return BadRequest("companyId inválido");
            if (fromUtc == default || toUtc == default) return BadRequest("fromUtc/toUtc obrigatórios");
            if (toUtc <= fromUtc) return BadRequest("toUtc deve ser maior que fromUtc");

            var list = await _entries.ListByEmployeeAndPeriod(companyId, employeeId, fromUtc, toUtc);

            return Ok(list.Select(e => new
            {
                e.Id,
                e.CompanyId,
                e.EmployeeId,
                e.TimestampUtc,
                e.Type,
                e.Source,
                e.Note
            }));
        }
    }
}
