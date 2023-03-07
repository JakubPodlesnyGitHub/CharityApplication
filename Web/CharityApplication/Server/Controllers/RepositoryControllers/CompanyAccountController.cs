using Application.Cqrs.Commands.CompanyAccount;
using Application.Cqrs.Queries.CompanyAccount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CharityApplication.Server.Controllers.RepositoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetComapniesAccounts))]
        public async Task<IActionResult> GetComapniesAccounts()
        {
            var query = new GetCompanyAccountsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("companyaccount/{companyAccountId}")]
        public async Task<IActionResult> GetCompanyAccount(int companyAccountId)
        {
            var query = new GetCompanyAccountByIdQuery(companyAccountId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("topcompanyaccounts/{numberOfCompanies}")]
        public async Task<IActionResult> GetTopCompanyAccounts(int numberOfCompanies)
        {
            var query = new GetTopCompanyAccountsWithMostBadgePointsQuery(numberOfCompanies);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost(nameof(CreateCompanyAccount))]
        public async Task<IActionResult> CreateCompanyAccount([FromBody] CreateCompanyAccountCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("companyaccount/{companyAccountId}")]
        public async Task<IActionResult> DeleteCompanyAccountCommand(int companyAccountId)
        {
            var request = new DeleteCompanyAccountCommand(companyAccountId);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateCompanyAccount))]
        public async Task<IActionResult> UpdateCompanyAccount([FromBody] UpdateCompanyAccountCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}