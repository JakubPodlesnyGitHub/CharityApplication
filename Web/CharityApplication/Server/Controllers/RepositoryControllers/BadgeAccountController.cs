using Application.Cqrs.Commands.BadgeAccount;
using Application.Cqrs.Queries.BadgeAccount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BadgeAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BadgeAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetBadgesAccounts))]
        public async Task<IActionResult> GetBadgesAccounts()
        {
            var query = new GetBadgesAccountsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet(nameof(GetBadgeAccount))]
        public async Task<IActionResult> GetBadgeAccount([FromQuery] int badgeId, [FromQuery] int accountId)
        {
            var query = new GetBadgeAccountByIdQuery(accountId, badgeId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("accountbadges/{accountId}")]
        public async Task<IActionResult> GetAccountBadges(int accountId)
        {
            var query = new GetAccountBadgesByAccountIdQuery(accountId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(nameof(CreateBadgeAccount))]
        public async Task<IActionResult> CreateBadgeAccount([FromBody] CreateBadgeAccountCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete(nameof(DeleteBadgeAccount))]
        public async Task<IActionResult> DeleteBadgeAccount([FromQuery] int badgeId, [FromQuery] int accountId)
        {
            var request = new DeleteBadgeAccountCommand(accountId, badgeId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}