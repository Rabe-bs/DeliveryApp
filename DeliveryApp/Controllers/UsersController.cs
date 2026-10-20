using MediatR;
using Microsoft.AspNetCore.Mvc;
using DeliveryApp.Application.Features.Users.Commands.CreateUser;
using DeliveryApp.Application.Features.Users.Commands.UpdateUserProfile;
using DeliveryApp.Application.Features.Users.Commands.ToggleUserStatus;
using DeliveryApp.Application.Features.Users.Commands.DeleteUser;
using DeliveryApp.Application.Features.Users.Queries.GetUserById;
using DeliveryApp.Application.Features.Users.Queries.GetUsersList;

namespace DeliveryApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var userId = await _sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = userId }, userId);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetUserByIdQuery(id);
            var result = await _sender.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var query = new GetUsersListQuery(pageNumber, pageSize);
            var result = await _sender.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:guid}/profile")]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] UpdateUserProfileCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest("identity user not matched.");
            }

            await _sender.Send(command, cancellationToken);
            return NoContent();
        }

        // تفعيل تعطيل المستخدم
        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> ToggleStatus(Guid id, CancellationToken cancellationToken)
        {
            var command = new ToggleUserStatusCommand(id);
            var result = await _sender.Send(command, cancellationToken);
            return Ok(new { IsActive = result });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteUserCommand(id);
            await _sender.Send(command, cancellationToken);
            return NoContent();
        }
    }
}