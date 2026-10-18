using DeliveryApp.Application.Features.Wallets.Commands.Deposit;
using DeliveryApp.Application.Features.Wallets.Queries.GetBalance;
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

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositCommand command)
        {
            // لضمان الامان استخراج المعرف وارساله للكوماند
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new { message = "Invalid user token or unauthorized access." });
            }

            var result = await _mediator.Send(command);

            if (result == null)
            {
                return BadRequest(new { message = "Deposit operation failed." });
            }

            return Ok(result);
        }
    }
}