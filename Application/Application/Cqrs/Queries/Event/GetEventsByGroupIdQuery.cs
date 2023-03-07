using Application.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.Event
{
    public class GetEventsByGroupIdQuery : IRequest<List<BasicEventDTO>>
    {
        public int IdGroup { get; set; }

        public GetEventsByGroupIdQuery(int idGroup)
        {
            IdGroup = idGroup;
        }
    }
}