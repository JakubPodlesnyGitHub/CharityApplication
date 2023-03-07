using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.BadgeGroup
{
    public class DeleteBadgeGroupCommand : IRequest<BasicBadgeGroupDTO>
    {
        public int IdGroup { get; set; }
        public int IdBadge { get; set; }

        public DeleteBadgeGroupCommand(int idGroup, int idBadge)
        {
            IdGroup = idGroup;
            IdBadge = idBadge;
        }
    }
}