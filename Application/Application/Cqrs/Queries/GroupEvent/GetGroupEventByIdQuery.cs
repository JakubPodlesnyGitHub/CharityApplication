using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.GroupEvent
{
    public class GetGroupEventByIdQuery : IRequest<BasicGroupEventDTO>
    {
        public int IdGroup { get; set; }
        public int IdEvent { get; set; }

        public GetGroupEventByIdQuery(int idGroup, int idEvent)
        {
            IdGroup = idGroup;
            IdEvent = idEvent;
        }
    }
}