using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.EventAnnouncement
{
    public class GetEventsAnnouncementsQuery : IRequest<List<BasicEventAnnouncementDTO>>
    {
    }
}