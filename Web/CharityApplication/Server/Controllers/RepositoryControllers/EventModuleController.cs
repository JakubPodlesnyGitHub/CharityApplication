using Application.Cqrs.Commands.EventModule;
using Application.Cqrs.Queries.EventModule;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventModuleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventModuleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetEventsModules))]
        public async Task<IActionResult> GetEventsModules()
        {
            var query = new GetEventsModulesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("eventmodule/{eventModuleId}")]
        public async Task<IActionResult> GetEventModule(int eventModuleId)
        {
            var query = new GetEventModuleByIdQuery(eventModuleId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("eventmodules/{eventId}")]
        public async Task<IActionResult> GetEventModulesByEventId(int eventId)
        {
            var query = new GetEventModulesListByEventIdQuery(eventId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(nameof(CreateEventModule))]
        public async Task<IActionResult> CreateEventModule([FromBody] CreateEventModuleCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("eventmodule/{eventModuleId}")]
        public async Task<IActionResult> DeleteEventModule(int eventModuleId)
        {
            var request = new DeleteEventModuleCommand(eventModuleId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateEventModuleCommand))]
        public async Task<IActionResult> UpdateEventModule([FromBody] UpdateEventModuleCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}