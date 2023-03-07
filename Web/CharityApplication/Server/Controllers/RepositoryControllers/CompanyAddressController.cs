using Application.Cqrs.Commands.CompanyAddress;
using Application.Cqrs.Queries.ComapnyAddress;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyAddressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyAddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetCompaniesAddresses))]
        public async Task<IActionResult> GetCompaniesAddresses()
        {
            var query = new GetCompaniesAddressesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("companyaddress/{companyAddressId}")]
        public async Task<IActionResult> GetCompanyAddress(int companyAddressId)
        {
            var query = new GetCompanyAddressByIdQuery(companyAddressId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(nameof(CreateCompanyAddress))]
        public async Task<IActionResult> CreateCompanyAddress([FromBody] CreateCompanyAddressCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("companyaddress/{companyAddressId}")]
        public async Task<IActionResult> DeleteCompanyAddress(int companyAddressId)
        {
            var request = new DeleteCompanyAddressCommand(companyAddressId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateCompanyAddress))]
        public async Task<IActionResult> UpdateCompanyAddress([FromBody] UpdateCompanyAddressCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}