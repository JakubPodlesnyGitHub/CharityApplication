using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.EventAccount
{
    public class GetEventsAccountsQuery : IRequest<List<BasicEventAccountDTO>>
    {
    }
}