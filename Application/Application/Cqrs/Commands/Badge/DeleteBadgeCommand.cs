using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.Badge
{
    public class DeleteBadgeCommand : IRequest<BasicBadgeDTO>
    {
        public int IdBadge { get; set; }

        public DeleteBadgeCommand(int idBadge)
        {
            IdBadge = idBadge;
        }
    }
}