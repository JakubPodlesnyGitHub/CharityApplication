using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Group
{
    public class GetGroupsByEventIdQuery : IRequest<List<BasicGroupDTO>>
    {
        public int IdEvent { get; set; }

        public GetGroupsByEventIdQuery(int idEvent)
        {
            IdEvent = idEvent;
        }
    }
}