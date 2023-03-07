using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.EventAnnouncement
{
    public class GetEventAnnouncementsByEventIdQuery : IRequest<List<BasicEventAnnouncementDTO>>
    {
        public int IdEvent { get; set; }

        public GetEventAnnouncementsByEventIdQuery(int idEvent)
        {
            IdEvent = idEvent;
        }
    }
}