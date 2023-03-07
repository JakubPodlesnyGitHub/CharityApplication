using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Commands.GroupAnnouncement
{
    public class DeleteGroupAnnouncementCommand : IRequest<BasicGroupAnnouncementDTO>
    {
        public int IdGroupAnnouncement { get; set; }

        public DeleteGroupAnnouncementCommand(int idGroupAnnouncement)
        {
            IdGroupAnnouncement = idGroupAnnouncement;
        }
    }
}