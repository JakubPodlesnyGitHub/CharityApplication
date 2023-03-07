using Application.Dtos.ExtendedDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Group
{
    public class GetTopGroupsWithMostBadgePointsQuery : IRequest<List<GroupWithBadgePointsDTO>>
    {
        public int NumberOfGroups { get; set; }

        public GetTopGroupsWithMostBadgePointsQuery(int numberOfGroups)
        {
            NumberOfGroups = numberOfGroups;
        }
    }
}