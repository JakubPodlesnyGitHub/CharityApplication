using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.GroupEvent
{
    public class GetEventGroupsByEventIdQuery : IRequest<List<BasicGroupEventDTO>>
    {
        public int IdEvent { get; set; }

        public GetEventGroupsByEventIdQuery(int idEvent)
        {
            IdEvent = idEvent;
        }
    }
}