using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.EventAnnouncement
{
    public class DeleteEventAnnouncementCommand : IRequest<BasicEventAnnouncementDTO>
    {
        public int IdEventAnnouncement { get; set; }

        public DeleteEventAnnouncementCommand(int idEventAnnouncement)
        {
            IdEventAnnouncement = idEventAnnouncement;
        }
    }
}