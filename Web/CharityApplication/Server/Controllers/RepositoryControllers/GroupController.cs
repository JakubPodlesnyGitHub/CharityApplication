using Application.Cqrs.Commands.Group;
using Application.Cqrs.Queries.Group;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetGroups))]
        public async Task<IActionResult> GetGroups()
        {
            var query = new GetGroupsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("groups-by-account/{accountId}")]
        public async Task<IActionResult> GetGroupsByAccountId(int accountId)
        {
            var query = new GetGroupsByAccountIdQuery(accountId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("groups-by-event/{eventId}")]
        public async Task<IActionResult> GetGroupsByEventId(int eventId)
        {
            var query = new GetGroupsByEventIdQuery(eventId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet(nameof(GetVisibleGroups))]
        public async Task<IActionResult> GetVisibleGroups()
        {
            var query = new GetVisibleGroups();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("public-private-groups/{accountId}")]
        public async Task<IActionResult> GetPublicPrivateGroups(int accountId)
        {
            var query = new GetPublicPrivateGroupsQuery(accountId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("group/{groupId}")]
        public async Task<IActionResult> GetGroup(int groupId)
        {
            var query = new GetGroupByIdQuery(groupId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("topgroups/{numberOfGroups}")]
        public async Task<IActionResult> GetTopPrivateAccounts(int numberOfGroups)
        {
            var query = new GetTopGroupsWithMostBadgePointsQuery(numberOfGroups);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(nameof(CreateGroup))]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("group/{groupId}")]
        public async Task<IActionResult> DeleteGroup(int groupId)
        {
            var request = new DeleteGroupCommand(groupId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateGroup))]
        public async Task<IActionResult> UpdateGroup([FromBody] UpdateGroupCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}