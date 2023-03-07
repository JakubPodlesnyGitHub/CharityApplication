using Application.Cqrs.Commands.GroupAnnouncement;
using Application.Cqrs.Queries.GroupAnnouncement;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupAnnouncementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupAnnouncementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetGroupsAnnouncements))]
        public async Task<IActionResult> GetGroupsAnnouncements()
        {
            var request = new GetGroupsAnnouncementsQuery();
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("groupannouncements-by-group/{groupId}")]
        public async Task<IActionResult> GetGroupAnnouncements(int groupId)
        {
            var request = new GetGroupAnnouncementsByGroupIdQuery(groupId);
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("groupannouncement/{groupannouncementId}")]
        public async Task<IActionResult> GetGroupAnnouncement(int groupannouncementId)
        {
            var request = new GetGroupAnnouncementByIdQuery(groupannouncementId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost(nameof(CreateGroupAnnouncement))]
        public async Task<IActionResult> CreateGroupAnnouncement([FromBody] CreateGroupAnnouncementCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("groupannouncement/{groupannouncementId}")]
        public async Task<IActionResult> DleteGroupAnnouncement(int groupannouncementId)
        {
            var request = new DeleteGroupAnnouncementCommand(groupannouncementId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateGroupAnnouncement))]
        public async Task<IActionResult> UpdateGroupAnnouncement([FromBody] UpdateGroupAnnouncementCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}