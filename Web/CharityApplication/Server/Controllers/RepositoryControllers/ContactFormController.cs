using Application.Cqrs.Commands.ContactForm;
using Application.Cqrs.Queries.ContactForm;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactFormController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactFormController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetContactForms))]
        public async Task<IActionResult> GetContactForms()
        {
            var query = new GetAllContactFormsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("contactform/{contactFormId}")]
        public async Task<IActionResult> GetContactForms(int contactFormId)
        {
            var query = new GetContactFormByIdQuery(contactFormId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost(nameof(CreateContactForm))]
        public async Task<IActionResult> CreateContactForm([FromBody] CreateContactFormCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateContactForm))]
        public async Task<IActionResult> UpdateContactForm([FromBody] UpdateContactFormCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("contactform/{contactFormId}")]
        public async Task<IActionResult> DeleteContactForm(int contactFormId)
        {
            var request = new DeleteContactFormCommand(contactFormId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}