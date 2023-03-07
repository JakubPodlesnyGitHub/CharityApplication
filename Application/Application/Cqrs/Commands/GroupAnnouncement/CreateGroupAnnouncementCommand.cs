using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.GroupAnnouncement
{
    public class CreateGroupAnnouncementCommand : IRequest<BasicGroupAnnouncementDTO>
    {
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;
        public int IdGroup { get; set; }
        public int IdOwner { get; set; }

        public CreateGroupAnnouncementCommand(string subject, string message, int idGroup, int idOwner)
        {
            Subject = subject;
            Message = message;
            IdGroup = idGroup;
            IdOwner = idOwner;
        }
    }
}