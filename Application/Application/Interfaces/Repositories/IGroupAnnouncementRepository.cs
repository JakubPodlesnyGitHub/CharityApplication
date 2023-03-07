using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IGroupAnnouncementRepository : IBaseRepository<GroupAnnouncement>
    {
        public Task<List<GroupAnnouncement>> GetGroupAnnouncementsByGroupId(int groupId);
    }
}