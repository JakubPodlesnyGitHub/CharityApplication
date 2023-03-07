using CharityApplication.Shared.Dtos.BasicDtos.Responses;
using MediatR;

namespace Application.Cqrs.Queries.GroupAnnouncement
{
    public class GetGroupsAnnouncementsQuery : IRequest<List<BasicGroupAnnouncementDTO>>
    {
    }
}