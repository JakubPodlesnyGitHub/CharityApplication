using Application.Cqrs.Commands.Status;
using Application.Cqrs.Queries.Status;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetEventStatutes))]
        public async Task<IActionResult> GetEventStatutes()
        {
            var query = new GetStatusesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("status/{statusId}")]
        public async Task<IActionResult> GetEventStatus(int statusId)
        {
            var query = new GetStatusByIdQuery(statusId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(nameof(CreateEventStatus))]
        public async Task<IActionResult> CreateEventStatus([FromBody] CreateStatusCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("status/{statusId}")]
        public async Task<IActionResult> DeleteEventStatus(int statusId)
        {
            var request = new DeleteStatusCommand(statusId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateEventStatus))]
        public async Task<IActionResult> UpdateEventStatus([FromBody] UpdateStatusCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}