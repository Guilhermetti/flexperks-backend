using FlexPerks.Application.Commands.Transactions;
using FlexPerks.Application.Handlers;
using FlexPerks.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexPerks.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly CreditDebitHandler _handler;
        private readonly IPerkTransactionRepository _txRepo;

        public TransactionsController(
            CreditDebitHandler handler,
            IPerkTransactionRepository txRepo)
        {
            _handler = handler;
            _txRepo = txRepo;
        }

        [HttpPost("credit")]
        public async Task<IActionResult> Credit([FromBody] CreditDebitCommand cmd)
        {
            cmd.Type = "Credit";
            var id = await _handler.Handle(cmd);
            if (!_handler.IsValid)
                return BadRequest(_handler.Notifications);

            return Created($"/api/transactions/{id}", new { id });
        }

        [HttpPost("debit")]
        public async Task<IActionResult> Debit([FromBody] CreditDebitCommand cmd)
        {
            cmd.Type = "Debit";
            var id = await _handler.Handle(cmd);
            if (!_handler.IsValid)
                return BadRequest(_handler.Notifications);

            return Created($"/api/transactions/{id}", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int walletId)
        {
            var list = await _txRepo.ListByWalletId(walletId);
            return Ok(list.Select(t => new { t.Id, t.WalletId, t.Amount, t.Type, t.OccurredAt }));
        }
    }
}
