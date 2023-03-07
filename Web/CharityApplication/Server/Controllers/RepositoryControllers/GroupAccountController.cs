using Application.Cqrs.Commands.GroupAccount;
using Application.Cqrs.Queries.GroupAccount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetGroupsAccounts))]
        public async Task<IActionResult> GetGroupsAccounts()
        {
            var query = new GetGroupsAccountsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("groupaccount")]
        public async Task<IActionResult> GetGroupAccountById([FromQuery] int accountId, [FromQuery] int groupId)
        {
            var query = new GetGroupAccountByIdQuery(accountId, groupId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(nameof(CreateGroupAccount))]
        public async Task<IActionResult> CreateGroupAccount([FromBody] CreateGroupAccountCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete(nameof(DeleteGroupAccount))]
        public async Task<IActionResult> DeleteGroupAccount([FromQuery] int accountId, [FromQuery] int groupId)
        {
            var request = new DeleteGroupAccountCommand(accountId, groupId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}