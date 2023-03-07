using Application.Cqrs.Commands.EventAnnouncement;
using Application.Cqrs.Queries.EventAnnouncement;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventAnnouncementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventAnnouncementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetEventsAnnouncements))]
        public async Task<IActionResult> GetEventsAnnouncements()
        {
            var request = new GetEventsAnnouncementsQuery();
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("eventannouncements-by-event/{eventId}")]
        public async Task<IActionResult> GetEventAnnouncements(int eventId)
        {
            var request = new GetEventAnnouncementsByEventIdQuery(eventId);
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("eventannouncement/{eventannouncementId}")]
        public async Task<IActionResult> GetEventAnnouncement(int eventannouncementId)
        {
            var request = new GetEventAnnouncementByIdQuery(eventannouncementId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost(nameof(CreateEventAnnouncement))]
        public async Task<IActionResult> CreateEventAnnouncement([FromBody] CreateEventAnnouncementCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("eventannouncement/{eventannouncementId}")]
        public async Task<IActionResult> DleteEventAnnouncement(int eventannouncementId)
        {
            var request = new DeleteEventAnnouncementCommand(eventannouncementId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateEventAnnouncement))]
        public async Task<IActionResult> UpdateEventAnnouncement([FromBody] UpdateEventAnnouncementCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}