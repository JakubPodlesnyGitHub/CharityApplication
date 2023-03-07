using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.EventAnnouncement
{
    public class CreateEventAnnouncementCommand : IRequest<BasicEventAnnouncementDTO>
    {
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;
        public int IdEvent { get; set; }
        public int IdOwner { get; set; }

        public CreateEventAnnouncementCommand(string subject, string message, int idEvent, int idOwner)
        {
            Subject = subject;
            Message = message;
            IdEvent = idEvent;
            IdOwner = idOwner;
        }
    }
}