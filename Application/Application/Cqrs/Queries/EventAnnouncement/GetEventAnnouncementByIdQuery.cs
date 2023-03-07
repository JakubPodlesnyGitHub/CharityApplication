using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.EventAnnouncement
{
    public class GetEventAnnouncementByIdQuery : IRequest<BasicEventAnnouncementDTO>
    {
        public int IdEventAnnouncement { get; set; }

        public GetEventAnnouncementByIdQuery(int idEvnetAnnouncement)
        {
            IdEventAnnouncement = idEvnetAnnouncement;
        }
    }
}