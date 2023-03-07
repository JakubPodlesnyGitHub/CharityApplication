using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Group
{
    public class GetGroupsByAccountIdQuery : IRequest<List<BasicGroupDTO>>
    {
        public int IdAccount { get; set; }

        public GetGroupsByAccountIdQuery(int idAccount)
        {
            IdAccount = idAccount;
        }
    }
}