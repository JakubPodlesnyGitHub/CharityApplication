using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Group
{
    public class GetPublicPrivateGroupsQuery : IRequest<List<BasicGroupDTO>>
    {
        public int IdAccount { get; set; }

        public GetPublicPrivateGroupsQuery(int idAccount)
        {
            IdAccount = idAccount;
        }
    }
}