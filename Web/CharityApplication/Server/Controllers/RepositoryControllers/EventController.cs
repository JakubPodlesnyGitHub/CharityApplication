using Application.Cqrs.Commands.Event;
using Application.Cqrs.Queries.Event;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet(nameof(GetEvents))]
        public async Task<IActionResult> GetEvents()
        {
            var query = new GetEventsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetEvent(int eventId)
        {
            var query = new GetEventByIdQuery(eventId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("events-by-account/{accountId}")]
        public async Task<IActionResult> GetEventsByAccountId(int accountId)
        {
            var query = new GetEventsByAccountIdQuery(accountId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("events-by-group/{groupId}")]
        public async Task<IActionResult> GetEventsByGroupId(int groupId)
        {
            var query = new GetEventsByGroupIdQuery(groupId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(nameof(CreateEvent))]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("event/{eventId}")]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            var request = new DeleteEventCommand(eventId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateEvent))]
        public async Task<IActionResult> UpdateEvent([FromBody] UpdateEventCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateEventStatus))]
        public async Task<IActionResult> UpdateEventStatus([FromBody] UpdateEventStatusCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}