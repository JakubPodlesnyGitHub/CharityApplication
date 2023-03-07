using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.CompanyAccount
{
    public class GetCompanyAccountsQuery : IRequest<List<BasicAccountDTO>>
    {
    }
}