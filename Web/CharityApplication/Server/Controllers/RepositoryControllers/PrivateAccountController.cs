using Application.Cqrs.Commands.PrivateAccount;
using Application.Cqrs.Queries.PrivateAccounts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PrivateAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrivateAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetPrivateAccounts))]
        public async Task<IActionResult> GetPrivateAccounts()
        {
            var query = new GetPrivateAccountsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("topprivateaccounts/{numberOfPeople}")]
        public async Task<IActionResult> GetTopPrivateAccounts(int numberOfPeople)
        {
            var query = new GetTopPrivateAccountsWithMostBadgePointsQuery(numberOfPeople);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("privateaccount/{privateAccountId}")]
        public async Task<IActionResult> GetPrivateAccount(int privateAccountId)
        {
            var query = new GetPrivateAccountQuery(privateAccountId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(nameof(CreatePrivateAccount))]
        public async Task<IActionResult> CreatePrivateAccount([FromBody] CreatePrivateAccountCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("privateaccount/{privateAccountId}")]
        public async Task<IActionResult> DeletePrivateAccount(int privateAccountId)
        {
            var request = new DeletePrivateAccountCommand(privateAccountId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdatePrivateAccount))]
        public async Task<IActionResult> UpdatePrivateAccount([FromBody] UpdatePrivateAccountCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}