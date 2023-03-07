using Application.Cqrs.Commands.GroupEvent;
using Application.Cqrs.Queries.GroupEvent;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupEventController : ControllerBase
    {
        private IMediator _mediator;

        public GroupEventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetGroupsEvents))]
        public async Task<IActionResult> GetGroupsEvents()
        {
            var query = new GetGroupsEventsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("groupevent/{groupId}/{eventId}")]
        public async Task<IActionResult> GetGroupEventById([FromQuery] int groupId, [FromQuery] int eventId)
        {
            var query = new GetGroupEventByIdQuery(groupId, eventId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("eventgroups/{eventId}")]
        public async Task<IActionResult> GetGroupEventById([FromQuery] int eventId)
        {
            var query = new GetEventGroupsByEventIdQuery(eventId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(nameof(CreateGroupEvent))]
        public async Task<IActionResult> CreateGroupEvent([FromBody] CreateGroupEventCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete(nameof(DeleteGroupEvent))]
        public async Task<IActionResult> DeleteGroupEvent([FromQuery] int groupId, [FromQuery] int eventId)
        {
            var request = new DeleteGroupEventCommand(groupId, eventId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateGroupEvent))]
        public async Task<IActionResult> UpdateGroupEvent([FromBody] UpdateGroupEventCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}