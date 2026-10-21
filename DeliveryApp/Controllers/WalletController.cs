using DeliveryApp.Application.Features.Wallets.Commands.Deposit;
using DeliveryApp.Application.Features.Wallets.Queries.GetBalance;
using DeliveryApp.Application.Features.Wallets.Queries.GetWalletTransactions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DeliveryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WalletController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new { message = "Invalid user token or unauthorized access." });

            }
            // seand Query to Mediator and handel result 
            var query = new GetBalanceQuery(userId);
            var result = await _mediator.Send(query);

            return Ok(result);



        }
        //حركة المحفظة عشان محد يضيع فلوسه gg
        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactions(CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new { message = "Invalid user token or unauthorized access." });
            }

            var query = new GetWalletTransactionsQuery(userId);
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositCommand command, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new { message = "Invalid user token or unauthorized access." });
            }

            // بناء كومان آمن يمنع التلاعب بالمعرف
            var secureCommand = new DepositCommand(userId, command.Amount, command.Description);
            var result = await _mediator.Send(secureCommand, cancellationToken);

            if (!result)
            {
                return BadRequest(new { message = "Deposit operation failed." });
            }

            return Ok(new { message = "Deposit successful." });
        }
    }
}