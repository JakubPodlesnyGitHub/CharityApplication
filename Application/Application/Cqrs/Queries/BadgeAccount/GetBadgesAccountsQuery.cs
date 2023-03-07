using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.BadgeAccount
{
    public class GetBadgesAccountsQuery : IRequest<List<BasicBadgeAccountDTO>>
    {
    }
}