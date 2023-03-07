using Application.Cqrs.Commands.EventAccount;
using Application.Cqrs.Queries.EventAccount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetEventsAccounts))]
        public async Task<IActionResult> GetEventsAccounts()
        {
            var query = new GetEventsAccountsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("eventaccounts/{eventId}")]
        public async Task<IActionResult> GetEventAccounts([FromQuery] int eventId)
        {
            var query = new GetEventAccountsByEventIdQuery(eventId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(nameof(CreateEventAccount))]
        public async Task<IActionResult> CreateEventAccount([FromBody] CreateEventAccountCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete(nameof(DeleteEventAccount))]
        public async Task<IActionResult> DeleteEventAccount([FromQuery] int eventId, [FromQuery] int accountId)
        {
            var request = new DeleteEventAccountCommand(accountId, eventId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateEventAccount))]
        public async Task<IActionResult> UpdateEventAccount([FromBody] UpdateEventAccountCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateEventAccountSubsidy))]
        public async Task<IActionResult> UpdateEventAccountSubsidy([FromBody] UpdateEventAccountSubsidyCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}