using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Group
{
    public class GetGroupByIdQuery : IRequest<BasicGroupDTO>
    {
        public int IdGroup { get; set; }

        public GetGroupByIdQuery(int idGroup)
        {
            IdGroup = idGroup;
        }
    }
}