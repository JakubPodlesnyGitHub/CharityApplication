using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.GroupAnnouncement
{
    public class GetGroupAnnouncementsByGroupIdQuery : IRequest<List<BasicGroupAnnouncementDTO>>
    {
        public int IdGroup { get; set; }

        public GetGroupAnnouncementsByGroupIdQuery(int idGroup)
        {
            IdGroup = idGroup;
        }
    }
}