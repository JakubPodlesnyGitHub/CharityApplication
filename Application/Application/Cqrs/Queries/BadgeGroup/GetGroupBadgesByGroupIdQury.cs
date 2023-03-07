using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.BadgeGroup
{
    public class GetGroupBadgesByGroupIdQury : IRequest<List<BasicBadgeGroupDTO>>
    {
        public int IdGroup { get; set; }

        public GetGroupBadgesByGroupIdQury(int idGroup)
        {
            IdGroup = idGroup;
        }
    }
}