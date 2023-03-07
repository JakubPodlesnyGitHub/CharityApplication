using Application.Cqrs.Commands.GroupName;
using Application.Cqrs.Queries.GroupName;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupNameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupNameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetGroupNames))]
        public async Task<IActionResult> GetGroupNames()
        {
            var query = new GetGroupNamesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("groupname/{groupNameId}")]
        public async Task<IActionResult> GetAccount(int groupNameId)
        {
            var query = new GetGroupNameByIdQuery(groupNameId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(nameof(CreateGroupName))]
        public async Task<IActionResult> CreateGroupName([FromBody] CreateGroupNameCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("groupname/{groupNameId}")]
        public async Task<IActionResult> DeleteAccount(int groupNameId)
        {
            var request = new DeleteGroupNameCommand(groupNameId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateGroupName))]
        public async Task<IActionResult> UpdateGroupName([FromBody] UpdateGroupNameCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}