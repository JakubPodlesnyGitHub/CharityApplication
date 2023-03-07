using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.BadgeAccount
{
    public class DeleteBadgeAccountCommand : IRequest<BasicBadgeAccountDTO>
    {
        public int IdAccount { get; set; }
        public int IdBadge { get; set; }

        public DeleteBadgeAccountCommand(int idAccount, int idBadge)
        {
            IdAccount = idAccount;
            IdBadge = idBadge;
        }
    }
}