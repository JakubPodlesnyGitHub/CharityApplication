using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.GroupAnnouncement
{
    public class GetGroupAnnouncementByIdQuery : IRequest<BasicGroupAnnouncementDTO>
    {
        public int IdGroupAnnouncement { get; set; }

        public GetGroupAnnouncementByIdQuery(int idGroupAnnouncement)
        {
            IdGroupAnnouncement = idGroupAnnouncement;
        }
    }
}