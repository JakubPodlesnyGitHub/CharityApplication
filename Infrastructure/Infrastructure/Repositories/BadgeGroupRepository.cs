using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BadgeGroupRepository : BaseRepository<BadgeGroup>, IBadgeGroupRepository
    {
        private readonly CharityApplicationDbContext _charityApplicationDbContext;

        public BadgeGroupRepository(CharityApplicationDbContext charityApplicationDbContext) : base(charityApplicationDbContext)
        {
            _charityApplicationDbContext = charityApplicationDbContext;
        }

        public async Task<List<BadgeGroup>> GetGroupBadgesByGroupId(int groupId)
        {
            return await _charityApplicationDbContext.BadgeGroups.Where(ae => ae.IdGroup == groupId).ToListAsync();
        }
    }
}