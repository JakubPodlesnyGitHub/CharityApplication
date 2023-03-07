using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.PrivateAccounts
{
    public class GetPrivateAccountsQuery : IRequest<List<BasicAccountDTO>>
    {
    }
}