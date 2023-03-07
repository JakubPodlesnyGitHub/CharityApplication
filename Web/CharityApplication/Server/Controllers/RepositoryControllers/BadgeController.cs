using Application.Cqrs.Commands.Badge;
using Application.Cqrs.Queries.Badge;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BadgeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BadgeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetBadges))]
        [AllowAnonymous]
        public async Task<IActionResult> GetBadges()
        {
            var query = new GetBadgesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("badge/{badgeId}")]
        public async Task<IActionResult> GetBageById(int badgeId)
        {
            var query = new GetBadgeQueryId(badgeId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(nameof(CreateBadge))]
        public async Task<IActionResult> CreateBadge([FromBody] CreateBadgeCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("badge/{badgeId}")]
        public async Task<IActionResult> DeleteBadge(int badgeId)
        {
            var request = new DeleteBadgeCommand(badgeId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateBadge))]
        public async Task<IActionResult> UpdateBadge([FromBody] UpdateBadgeCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}