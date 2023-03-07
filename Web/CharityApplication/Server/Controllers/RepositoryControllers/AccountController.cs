using Application.Cqrs.Commands.Account;
using Application.Cqrs.Queries.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet(nameof(GetAccounts))]
        public async Task<IActionResult> GetAccounts()
        {
            var query = new GetAllAccountQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("top-accounts/{numberOfEvents}")]
        public async Task<IActionResult> GetAccountsWithMostCreatedEvents(int numberOfEvents)
        {
            var query = new GetTopAccountsWithMostCreatedEventsQuery(numberOfEvents);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("accounts-by-event/{eventId}")]
        public async Task<IActionResult> GetAccountsByEventId(int eventId)
        {
            var query = new GetAccountsByEventIdQuery(eventId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("accounts-by-group/{groupId}")]
        public async Task<IActionResult> GetAccountsByGroupId(int groupId)
        {
            var query = new GetAccountsByGroupIdQuery(groupId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("unconfirmed-accounts-by-event/{eventId}")]
        public async Task<IActionResult> GetUnconfirmedAccountsByEventId(int eventId)
        {
            var query = new GetUnConfirmedAccountsByEventId(eventId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetAccount(int accountId)
        {
            var query = new GetAccountByIdQuery(accountId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("account-by-email/{accountEmail}")]
        public async Task<IActionResult> GetAccountByEmail(string accountEmail)
        {
            var query = new GetAccountByEmailQuery(accountEmail);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(nameof(CreateAccount))]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("account/{accountId}")]
        public async Task<IActionResult> DeleteAccount(int accountId)
        {
            var request = new DeleteAccountCommand(accountId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateAccount))]
        public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdatePasswordAccount))]
        public async Task<IActionResult> UpdatePasswordAccount([FromBody] UpdatePasswordAccountCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}