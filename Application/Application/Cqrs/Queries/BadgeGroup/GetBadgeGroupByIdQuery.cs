using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.BadgeGroup
{
    public class GetBadgeGroupByIdQuery : IRequest<BasicBadgeGroupDTO>
    {
        public int IdGroup { get; set; }
        public int IdBadge { get; set; }

        public GetBadgeGroupByIdQuery(int idGroup, int idBadge)
        {
            IdGroup = idGroup;
            IdBadge = idBadge;
        }
    }
}