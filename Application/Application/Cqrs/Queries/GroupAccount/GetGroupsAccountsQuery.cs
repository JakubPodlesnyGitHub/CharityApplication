using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.GroupAccount
{
    public class GetGroupsAccountsQuery : IRequest<List<BasicGroupAccountDTO>>
    {
    }
}