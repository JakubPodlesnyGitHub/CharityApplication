using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.GroupAccount
{
    public class GetGroupAccountsByIdQuery : IRequest<List<BasicGroupAccountDTO>>
    {
        public int IdGroup { get; set; }

        public GetGroupAccountsByIdQuery(int idGroup)
        {
            IdGroup = idGroup;
        }
    }
}