using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GroupAnnouncementRepository : BaseRepository<GroupAnnouncement>, IGroupAnnouncementRepository
    {
        private readonly CharityApplicationDbContext _charityApplicationDbContext;

        public GroupAnnouncementRepository(CharityApplicationDbContext charityApplicationDbContext) : base(charityApplicationDbContext)
        {
            _charityApplicationDbContext = charityApplicationDbContext;
        }

        public async Task<List<GroupAnnouncement>> GetGroupAnnouncementsByGroupId(int groupId)
        {
            return await _charityApplicationDbContext.GroupAnnouncements.Where(g => g.IdGroup == groupId).ToListAsync();
        }
    }
}