using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Account
{
    public class GetAccountsByGroupIdQuery : IRequest<List<BasicAccountDTO>>
    {
        public int IdGroup { get; set; }

        public GetAccountsByGroupIdQuery(int idGroup)
        {
            IdGroup = idGroup;
        }
    }
}