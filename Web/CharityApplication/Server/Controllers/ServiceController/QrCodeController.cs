using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.ServiceController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QrCodeController : ControllerBase
    {
        private readonly IQrCodeService _qrCodeService;

        public QrCodeController(IQrCodeService qrCodeService)
        {
            _qrCodeService = qrCodeService;
        }

        [HttpPost(nameof(CreateQrCode))]
        public IActionResult CreateQrCode([FromBody] string url)
        {
            return Ok(_qrCodeService.GetBase64QrCodeString(url));
        }
    }
}