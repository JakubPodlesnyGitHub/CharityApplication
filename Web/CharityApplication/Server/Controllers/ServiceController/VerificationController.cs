using Application.Dtos.ServiceDtos.Requests;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.ServiceController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VerificationController : ControllerBase
    {
        private readonly IVerificationService _verificationService;

        public VerificationController(IVerificationService verificationService)
        {
            _verificationService = verificationService;
        }

        [HttpPut(nameof(VerifyAcccount))]
        public async Task<IActionResult> VerifyAcccount([FromBody] VerificationRequestDTO request)
        {
            var response = await _verificationService.VerifyAccount(request);
            return response.IsVerificationSuccessful ? Ok(response) : Unauthorized(response);
        }
    }
}