using Application.Dtos.ServiceDtos.Requests;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.ServiceController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost(nameof(SendEmail))]
        public async Task<IActionResult> SendEmail(EmailRequestDTO request)
        {
            await _emailService.SendEmailAsync(request);

            return Ok();
        }
    }
}