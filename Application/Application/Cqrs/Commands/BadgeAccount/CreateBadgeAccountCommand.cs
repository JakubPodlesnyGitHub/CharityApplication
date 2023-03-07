using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.BadgeAccount
{
    public class CreateBadgeAccountCommand : IRequest<BasicBadgeAccountDTO>
    {
        public int IdBadge { get; set; }
        public int IdAccount { get; set; }
    }
}