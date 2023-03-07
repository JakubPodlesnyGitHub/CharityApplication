using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.GroupAccount
{
    public class GetGroupAccountByIdQuery : IRequest<BasicGroupAccountDTO>
    {
        public int IdAccount { get; set; }
        public int IdGroup { get; set; }

        public GetGroupAccountByIdQuery(int idAccount, int idGroup)
        {
            IdAccount = idAccount;
            IdGroup = idGroup;
        }
    }
}