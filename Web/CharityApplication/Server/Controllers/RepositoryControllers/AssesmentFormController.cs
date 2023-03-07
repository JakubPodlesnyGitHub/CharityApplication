using Application.Cqrs.Commands.AssesmentForm;
using Application.Cqrs.Queries.AssesmentForm;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssesmentFormController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssesmentFormController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetAssesmentForms))]
        public async Task<IActionResult> GetAssesmentForms()
        {
            var query = new GetAssesmentFormsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("assesmentForms/{eventId}")]
        public async Task<IActionResult> GetAssesmentForms(int eventId)
        {
            var query = new GetAssesmentFormsByEventIdQuery(eventId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("assesmentForm/{assesmentFormId}")]
        public async Task<IActionResult> GetAccount(int assesmentFormId)
        {
            var query = new GetAssesmentFormByIdQuery(assesmentFormId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(nameof(CreateAssesmentForm))]
        public async Task<IActionResult> CreateAssesmentForm([FromBody] CreateAssesmentFormCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("assesmentForm/{assesmentFormId}")]
        public async Task<IActionResult> DeleteAssesmentForm(int assesmentFormId)
        {
            var request = new DeleteAssesmentFormCommand(assesmentFormId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateAssesmentForm))]
        public async Task<IActionResult> UpdateAssesmentForm([FromBody] UpdateAssesmentFormCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}