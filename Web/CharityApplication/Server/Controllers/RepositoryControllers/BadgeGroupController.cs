using Application.Cqrs.Commands.BadgeGroup;
using Application.Cqrs.Queries.BadgeGroup;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BadgeGroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BadgeGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetBadgesGroups))]
        public async Task<IActionResult> GetBadgesGroups()
        {
            var query = new GetBadgesGroupsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("badgegroup")]
        public async Task<IActionResult> GetBadgeGroupById([FromQuery] int groupId, [FromQuery] int badgeId)
        {
            var query = new GetBadgeGroupByIdQuery(groupId, badgeId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("groupbadges/{groupId}")]
        public async Task<IActionResult> GetGroupBadges([FromQuery] int groupId)
        {
            var query = new GetGroupBadgesByGroupIdQury(groupId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(nameof(CreateBadgeGroup))]
        public async Task<IActionResult> CreateBadgeGroup([FromBody] CreateBadgeGroupCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete(nameof(DeleteBadgeGroup))]
        public async Task<IActionResult> DeleteBadgeGroup([FromQuery] int groupId, [FromQuery] int badgeId)
        {
            var request = new DeleteBadgeGroupCommand(groupId, badgeId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}