using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.BadgeAccount
{
    public class GetBadgeAccountByIdQuery : IRequest<BasicBadgeAccountDTO>
    {
        public int IdAccount { get; set; }
        public int IdBadge { get; set; }

        public GetBadgeAccountByIdQuery(int idAccount, int idBadge)
        {
            IdAccount = idAccount;
            IdBadge = idBadge;
        }
    }
}