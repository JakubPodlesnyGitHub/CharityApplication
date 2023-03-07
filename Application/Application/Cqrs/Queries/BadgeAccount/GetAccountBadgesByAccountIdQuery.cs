using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.BadgeAccount
{
    public class GetAccountBadgesByAccountIdQuery : IRequest<List<BasicBadgeAccountDTO>>
    {
        public int IdAccount { get; set; }

        public GetAccountBadgesByAccountIdQuery(int idAccount)
        {
            IdAccount = idAccount;
        }
    }
}