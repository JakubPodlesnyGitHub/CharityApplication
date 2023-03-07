using Application.Cqrs.Commands.CompanyRepresentative;
using Application.Cqrs.Queries.CompanyRepresentative;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyRepresentativeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyRepresentativeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetCompaniesRepresentatives))]
        public async Task<IActionResult> GetCompaniesRepresentatives()
        {
            var query = new GetCompaniesRepresentativesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("companyrepresentative/{companyReperesentativeId}")]
        public async Task<IActionResult> GetCompanyRepresentative(int companyReperesentativeId)
        {
            var query = new GetComapnyRepresentativeByIdQuery(companyReperesentativeId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(nameof(CreateCompanyRepresentative))]
        public async Task<IActionResult> CreateCompanyRepresentative([FromBody] CreateCompanyRepresentativeCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateCompanyRepresentative))]
        public async Task<IActionResult> UpdateCompanyRepresentative([FromBody] UpdateCompanyRepresentativeCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("companyrepresentative/{companyReperesentativeId}")]
        public async Task<IActionResult> DeleteCompanyRepresentative(int companyReperesentativeId)
        {
            var request = new DeleteCompanyRepresentativeCommand(companyReperesentativeId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}