using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.BadgeGroup
{
    public class CreateBadgeGroupCommand : IRequest<BasicBadgeGroupDTO>
    {
        public int IdBadge { get; set; }
        public int IdGroup { get; set; }

        public CreateBadgeGroupCommand(int idBadge, int idGroup)
        {
            IdBadge = idBadge;
            IdGroup = idGroup;
        }
    }
}