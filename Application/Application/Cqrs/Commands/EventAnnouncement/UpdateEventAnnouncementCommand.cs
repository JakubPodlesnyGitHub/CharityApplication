using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.EventAnnouncement
{
    public class UpdateEventAnnouncementCommand : IRequest<BasicEventAnnouncementDTO>
    {
        public int IdEventAnnouncement { get; set; }
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;
        public int IdEvent { get; set; }
        public int IdOwner { get; set; }

        public UpdateEventAnnouncementCommand(int idEventAnnouncement, string subject, string message, int idEvent, int idOwner)
        {
            IdEventAnnouncement = idEventAnnouncement;
            Subject = subject;
            Message = message;
            IdEvent = idEvent;
            IdOwner = idOwner;
        }
    }
}