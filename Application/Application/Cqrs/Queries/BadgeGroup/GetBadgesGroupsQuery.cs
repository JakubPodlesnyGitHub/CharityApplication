using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.BadgeGroup
{
    public class GetBadgesGroupsQuery : IRequest<List<BasicBadgeGroupDTO>>
    {
    }
}