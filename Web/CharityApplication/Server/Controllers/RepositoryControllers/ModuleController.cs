using Application.Cqrs.Commands.Module;
using Application.Cqrs.Queries.Module;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ModuleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ModuleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet(nameof(GetModules))]
        public async Task<IActionResult> GetModules()
        {
            var query = new GetModulesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("module/{moduleId}")]
        public async Task<IActionResult> GetModule(int moduleId)
        {
            var query = new GetModuleByIdQuery(moduleId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(nameof(CreateModule))]
        public async Task<IActionResult> CreateModule([FromBody] CreateModuleCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("module/{moduleId}")]
        public async Task<IActionResult> DeleteModule(int moduleId)
        {
            var request = new DeleteModuleCommand(moduleId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateModule))]
        public async Task<IActionResult> UpdateModule([FromBody] UpdateModuleCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}